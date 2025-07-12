using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data.Collaboration;
using ClockingSystemReminder.Data.Ticketing;
using Microsoft.Win32;
using Newtonsoft.Json;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    //TODO: Allow meeting threshold (ie: if meeting start 5 minutes early...)

    public partial class TimeRegistrationForm : Form, IDisposable
    {
        readonly TimeSpan workTime;
        readonly DateTime registerDate; //In local time!
        readonly DateTime registerDateUTC;

        readonly ITicketingSystem ticketingSystem;
        readonly ICollaborationSystem collaborationSystem;

        readonly List<CallRecord> calls;
        readonly List<TimeRegistration> callRegistrations;
        readonly List<TimeRegistration> manualRegistrations;
        readonly List<TimeRegistration> meetingRegistrations;
        readonly RegistryKey recentTicketsRegistryKey;

        bool hasOffwork;
        TimeSpan realWorkTime; //Time spent NOT in calls/meetings
        int rightClickedRowIndex;
        TicketPortfolio ticketPortfolio;

        public TimeRegistrationForm(DateTime registerDate, TimeSpan workTime,
                                    ICollaborationSystem collaborationSystem, ITicketingSystem ticketingSystem)
        {
            InitializeComponent();
            this.workTime = workTime;
            this.registerDate = registerDate;
            this.ticketingSystem = ticketingSystem;
            this.collaborationSystem = collaborationSystem;
            this.registerDateUTC = registerDate.ToUniversalTime();

            this.calls = new List<CallRecord>();
            this.callRegistrations = new List<TimeRegistration>();
            this.manualRegistrations = new List<TimeRegistration>();
            this.meetingRegistrations = new List<TimeRegistration>();
            this.recentTicketsRegistryKey = Utils.OpenSystemRegistryKey("RecentTickets");
        }

        private void HourRegistration_Load(object sender, EventArgs e)
        {
            var roundedWorkTime = GetRoundedTimeSpan(workTime);

            collaborationSystem.Init();

            var totalMeetingTime = LoadMeetings();
            var totalCallTime = LoadCalls();

            ticketingSystem.Init();
            LoadTicketPortfolio();

            realWorkTime = roundedWorkTime - (totalMeetingTime + totalCallTime + TimeSpan.FromMinutes(30));
            if (realWorkTime < TimeSpan.Zero)
            {
                MessageBox.Show("Warning: It seems some meeting or call went on for longer than expected?",
                                "Warning: Long meeting?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                realWorkTime = TimeSpan.Zero;
            }
            if (roundedWorkTime.TotalHours < ClockingManager.WORK_HOURS)
            {
                var missingTime = TimeSpan.FromHours(ClockingManager.WORK_HOURS) - roundedWorkTime;
                var rows = manualRegistrationsView.Rows;
                rows.Add(1);

                var rowCells = rows[0].Cells;
                var offworkRegistration = new TimeRegistration(missingTime, "Flex time");
                rowCells[0].Value = FormatTimeSpan(offworkRegistration);
                rowCells[2].Value = offworkRegistration.Description;
                manualRegistrations.Add(offworkRegistration);

                hasOffwork = true;
            }

            callsBox.Text = $"Calls (Total: {FormatShortTimeSpan(totalCallTime)})";
            meetingsBox.Text = $"Meetings (Total: {FormatShortTimeSpan(totalMeetingTime)})";
            OnManualRegistrationUpdate();
            this.Text = $"Time Registration | {registerDate.ToString("dd/MM/yyyy")} | Time worked: {roundedWorkTime}";
        }

        private string FormatShortTimeSpan(TimeSpan timeSpan)
        {
            return timeSpan.ToString("h':'mm");
        }

        private void OnManualRegistrationUpdate()
        {
            var remainingDuration = GetRemainingDuration();
            manualRegistrationsBox.Text = $"Manual Registrations (Remaining {FormatShortTimeSpan(remainingDuration)})";
            manualRegistrationsView.AllowUserToAddRows = remainingDuration != TimeSpan.Zero;
        }

        private TimeSpan GetRemainingDuration()
        {
            var remainingDuration = realWorkTime;
            var startIndex = hasOffwork ? 1 : 0;
            for (int currIndex = startIndex; currIndex < manualRegistrations.Count; currIndex++)
            {
                var manualRegistration = manualRegistrations[currIndex];
                remainingDuration -= manualRegistration.Duration;
            }
            return remainingDuration;
        }

        private void LoadTicketPortfolio()
        {
            var recentTickets = LoadRecentTickets();
            var favouriteTickets = ticketingSystem.GetFavoriteTickets();
            var assignedTickets = ticketingSystem.GetAssignedTickets();
            this.ticketPortfolio = new TicketPortfolio(recentTickets, favouriteTickets, assignedTickets);

            foreach (var favoriteTicket in favouriteTickets)
            {
                var text = favoriteTicket.ToStringShort(15);
                var dropDownItem = contextMenu.Items.Add(text);
                dropDownItem.Click += FavoriteMenuItem_Click;
            }
        }

        private IList<TicketInfo> LoadRecentTickets()
        {
            //Caveat: It will be a problem if you suddenly switch ticketing system :P
            var result = new List<TicketInfo>();
            for (int index = 0; index < TicketPortfolio.MAX_RECENT_TICKETS; index++)
            {
                var recentTicketData = recentTicketsRegistryKey.GetValue($"Item{index}");
                if (recentTicketData != null)
                {
                    var recentTicket = TicketInfo.FromJson(recentTicketData.ToString());
                    result.Add(recentTicket);
                }
            }
            return result;
        }

        private TimeSpan LoadMeetings()
        {
            var totalDuration = TimeSpan.Zero;
            var nextDay = registerDateUTC.AddDays(1);
            var calendarEvents = collaborationSystem.GetCalendarEvents(registerDateUTC, nextDay);
            foreach (var calendarEvent in calendarEvents)
            {
                var call = TryGetCallForCalendarEvent(calendarEvent);
                if (call == null)
                {
                    continue;
                }

                var hasParticipated = call.HasParticipant(collaborationSystem.MyUser);
                if (!hasParticipated)
                {
                    continue;
                }

                var roundedDuration = GetRoundedTimeSpan(call.Duration);
                if (roundedDuration == TimeSpan.Zero)
                {
                    continue;
                }

                var eventTitle = calendarEvent.Title;
                var startTime = calendarEvent.Period.StartTime;
                RegisterCall(startTime, roundedDuration, eventTitle, eventTitle, meetingsView, meetingRegistrations);
                totalDuration += roundedDuration;
            }
            meetingsView.ClearSelection();
            return totalDuration;
        }

        private CallRecord TryGetCallForCalendarEvent(CalendarEvent calendarEvent)
        {
            var retry = true;
            while (retry)
            {
                try
                {
                    return collaborationSystem.GetCallForCalendarEvent(calendarEvent);
                }
                catch (WebException ex)
                {
                    if (WebUtils.WebExceptionHasStatusCode(ex, HttpStatusCode.Forbidden))
                    {
                        MessageBox.Show($"Unable to retrieve the call for the \"{calendarEvent.Title}\" calendar event!", "Error retrieving call", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    retry = WebUtils.ShowNetworkRetryMessage(ex);
                }
                catch (Exception ex)
                {
                    retry = Utils.ShowErrorRetryMessage(ex);
                }
            }
            return null;
        }

        private TimeSpan LoadCalls()
        {
            var totalDuration = TimeSpan.Zero;
            var nextDay = registerDateUTC.AddDays(1);
            var callHistory = collaborationSystem.GetCallHistory();
            foreach (var call in callHistory)
            {
                var startTime = call.Period.StartTime;
                if (startTime < registerDateUTC)
                {
                    //Stop processing any remaining calls
                    break;
                }

                var roundedDuration = GetRoundedTimeSpan(call.Duration);
                if (roundedDuration == TimeSpan.Zero)
                {
                    continue;
                }

                if (startTime >= nextDay)
                {
                    continue;
                }

                var participants = FormatParticipants(call.Participants);
                RegisterCall(startTime, roundedDuration, participants, null, callsView, callRegistrations);
                totalDuration += roundedDuration;
                calls.Add(call);
            }
            callsView.ClearSelection();
            return totalDuration;
        }

        private void RegisterCall(DateTime displayStartTime, TimeSpan roundedDuration, string titleText, string descriptionText, DataGridView targetView, IList<TimeRegistration> targetList)
        {
            var timeRegistration = new TimeRegistration(roundedDuration, descriptionText);

            targetView.Rows.Add(
                FormatUTCDateTime(displayStartTime),
                FormatTimeSpan(timeRegistration),
                titleText,
                string.Empty,
                descriptionText
            );
            targetList.Add(timeRegistration);
        }

        private string FormatParticipants(IEnumerable<UserInfo> participants)
        {
            var otherParticipants = participants.Where(p => p != collaborationSystem.MyUser)
                                                .Select(p => p.FullName);
            return string.Join("; ", otherParticipants);
        }

        private TimeSpan GetRoundedTimeSpan(TimeSpan timeSpan)
        {
            var totalSeconds = (int)timeSpan.TotalSeconds;
            var roundedSeconds = Utils.RoundToClosestMultiple(totalSeconds, 15 * 60); //15 minute intervals
            return TimeSpan.FromSeconds(roundedSeconds);
        }

        private string FormatUTCDateTime(DateTime utcDateTime)
        {
            var localTime = utcDateTime.ToLocalTime();
            return localTime.ToString("HH:mm");
        }

        private string FormatTimeSpan(TimeRegistration timeRegistration)
        {
            return FormatTimeSpan(timeRegistration.Hours, timeRegistration.Minutes);
        }

        private string FormatTimeSpan(int hours, int minutes)
        {
            var result = new StringBuilder();
            if (hours != 0)
            {
                result.Append(hours);
                result.Append("h");
            }

            if (minutes != 0)
            {
                result.Append(minutes);
                result.Append("m");
            }
            return result.ToString();
        }

        private void meetingsView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleCallEdit(meetingsView, meetingRegistrations, e.RowIndex);
        }

        private void callsView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //TODO: Show chat window | TODO: Make a sort of "iterator" helper
            //var chatHistory = collaborationSystem.GetChatHistory(callRecord.ChatID);
            HandleCallEdit(callsView, callRegistrations, e.RowIndex);
        }

        private void HandleCallEdit(DataGridView dataGridView, IList<TimeRegistration> timeRegistrations, int rowIndex)
        {
            var timeRegistration = timeRegistrations[rowIndex];
            var saved = OpenTicketRegistration(timeRegistration, null);
            if (saved)
            {
                UpdateCallCells(dataGridView, timeRegistration, rowIndex);
                EnableSaveButtonCheck();
            }
        }

        private void UpdateCallCells(DataGridView dataGridView, TimeRegistration timeRegistration, int rowIndex)
        {
            var rowCells = dataGridView.Rows[rowIndex].Cells;
            rowCells[3].Value = timeRegistration.Ticket?.ToString();
            rowCells[4].Value = timeRegistration.Description;
        }

        private void meetingsView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                rightClickedRowIndex = e.RowIndex;
                contextMenu.Show(Form.MousePosition);
                meetingsView.ClearSelection();
                meetingsView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void FavoriteMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripItem)sender;
            var ticketIndex = contextMenu.Items.IndexOf(menuItem);
            var favoriteTicket = ticketPortfolio.FavoriteTickets.ElementAt(ticketIndex);

            var rowIndex = rightClickedRowIndex;
            var timeRegistration = meetingRegistrations[rowIndex];
            timeRegistration.Ticket = favoriteTicket;
            UpdateCallCells(meetingsView, timeRegistration, rowIndex);
            EnableSaveButtonCheck();
        }

        private void manualRegistrationsView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            EditManualRegistration(e.RowIndex);
        }

        private void EditManualRegistration(int rowIndex)
        {
            var newRegistration = rowIndex == manualRegistrations.Count;
            var timeRegistration = !newRegistration ? manualRegistrations[rowIndex] : new TimeRegistration();

            var allowTimeRegistration = !hasOffwork || rowIndex != 0;
            var allocatableTime = allowTimeRegistration ? GetAllocatableTime(timeRegistration)
                                                        : (TimeSpan?)null;
            var saved = OpenTicketRegistration(timeRegistration, allocatableTime);
            if (saved)
            {
                if (newRegistration)
                {
                    manualRegistrationsView.Rows.Add(1);
                    manualRegistrations.Add(timeRegistration);
                }

                var rowCells = manualRegistrationsView.Rows[rowIndex].Cells;
                rowCells[0].Value = FormatTimeSpan(timeRegistration);
                rowCells[1].Value = timeRegistration.Ticket.ToString();
                rowCells[2].Value = timeRegistration.Description;
                OnManualRegistrationUpdate();
                EnableSaveButtonCheck();
            }
        }

        private bool OpenTicketRegistration(TimeRegistration timeRegistration, TimeSpan? allocatableTime)
        {
            using (var ticketDialog = new TicketRegistrationDialog(ticketingSystem, ticketPortfolio, timeRegistration, allocatableTime, realWorkTime))
            {
                if (ticketDialog.ShowDialog() == DialogResult.OK)
                {
                    var selectedTicket = ticketDialog.SelectedTicket;

                    timeRegistration.Ticket = selectedTicket;
                    timeRegistration.Description = ticketDialog.Description;
                    if (allocatableTime != null)
                    {
                        timeRegistration.Minutes = ticketDialog.Minutes;
                        timeRegistration.Hours = ticketDialog.Hours;
                    }
                    if (!ticketPortfolio.IsFavoriteTicket(selectedTicket))
                    {
                        AddTicketToRecentTickets(selectedTicket);
                    }
                    return true;
                }
            }
            return false;
        }

        private void AddTicketToRecentTickets(TicketInfo ticket)
        {
            var recentTickets = ticketPortfolio.RecentTickets;
            if (recentTickets.Contains(ticket))
            {
                return;
            }
            if (recentTickets.Count == TicketPortfolio.MAX_RECENT_TICKETS)
            {
                //Remove oldest item
                recentTickets.RemoveAt(0);
            }
            recentTickets.Add(ticket);
        }

        private TimeSpan GetAllocatableTime(TimeRegistration timeRegistration)
        {
            return GetRemainingDuration() + timeRegistration.Duration;
        }

        private void manualRegistrationsView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }

            var selectedRows = manualRegistrationsView.SelectedRows;
            if (selectedRows.Count == 0)
            {
                return;
            }

            var rowIndex = selectedRows[0].Index;
            if (rowIndex == manualRegistrations.Count)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                EditManualRegistration(rowIndex);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (hasOffwork && rowIndex == 0)
                {
                    return;
                }
                manualRegistrationsView.Rows.RemoveAt(rowIndex);
                manualRegistrations.RemoveAt(rowIndex);
                OnManualRegistrationUpdate();
                saveButton.Enabled = false;
            }
        }

        private void EnableSaveButtonCheck()
        {
            saveButton.Enabled = CanEnableSaveButton();
        }

        private bool CanEnableSaveButton()
        {
            if (!AllRegistrationsHaveATicket(meetingRegistrations) ||
                !AllRegistrationsHaveATicket(callRegistrations) ||
                !AllRegistrationsHaveATicket(manualRegistrations))
            {
                return false;
            }
            var remainingDuration = GetRemainingDuration();
            return remainingDuration == TimeSpan.Zero;
        }

        private bool AllRegistrationsHaveATicket(IList<TimeRegistration> timeRegistrations)
        {
            return timeRegistrations.All(tr => tr.Ticket != null);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var registrations = meetingRegistrations.Concat(callRegistrations)
                                                    .Concat(manualRegistrations)
                                                    .ToList();

            Func<ProgressDelegate, bool> task = progressCallback =>
            {
                var totalCount = registrations.Count;
                for (int index = 0; index < totalCount; index++)
                {
                    var percentage = (index * 100) / totalCount;
                    progressCallback($"Registered {index}/{totalCount}", percentage);

                    var timeRegistration = registrations[index];
                    if (!RegisterHour(timeRegistration))
                    {
                        MessageBox.Show("Failed to register hours. Try registering them manually instead.\n\nHint: Use CTRL+C to copy the details of a selected row", "Failed to register hours.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                progressCallback($"Registered {totalCount}/{totalCount}", 100);
                MessageBox.Show("All hours have been successfully registered!", "Hours registered.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            };

            using (var progressDialog = new ProgressDialog(task))
            {
                progressDialog.ShowDialog();
                if (progressDialog.Success)
                {
                    SaveRecentTickets();
                    this.Close();
                }
            }
        }

        private bool RegisterHour(TimeRegistration timeRegistration)
        {
            var retry = true;
            while (retry)
            {
                try
                {
                    ticketingSystem.RegisterHours(registerDate, timeRegistration);
                    return true;
                }
                catch (WebException ex)
                {
                    retry = WebUtils.ShowNetworkRetryMessage(ex);
                }
                catch (Exception ex)
                {
                    retry = Utils.ShowErrorRetryMessage(ex);
                }
            }
            return false;
        }

        private void SaveRecentTickets()
        {
            var recentTickets = ticketPortfolio.RecentTickets;
            for (int index = 0; index < recentTickets.Count; index++)
            {
                var recentTicket = recentTickets[index];
                var recentTicketData = JsonConvert.SerializeObject(recentTicket);
                recentTicketsRegistryKey.SetValue($"Item{index}", recentTicketData);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                recentTicketsRegistryKey.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
