using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data.Ticketing;
using ClockingSystemReminder.Extensions;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    public partial class TicketRegistrationDialog : Form
    {
        const int MAX_MINUTEBOX_INDEX = 3; //ie: Minute "00"

        readonly TimeSpan allocatableTime;
        readonly TicketPortfolio ticketPortfolio;
        readonly ITicketingSystem ticketingSystem;

        int totalFractions;
        bool ignoreNewSelection;
        TicketInfo selectedTicket;
        int maxHourMinMinuteIndex; //The minimum minute index for the max hour. NOTE: *Minimum* limit, since the minutes are ordered in descending order!
        int prevMinuteSelectedIndex;
        IList<TicketInfo> searchResults;

        public TicketRegistrationDialog(ITicketingSystem ticketingSystem, TicketPortfolio ticketPortfolio,
                                        TimeRegistration timeRegistration, TimeSpan? allocatableTime, TimeSpan realWorkTime)
        {
            this.ticketingSystem = ticketingSystem;
            this.ticketPortfolio = ticketPortfolio;

            InitializeComponent();
            PopulateTickets(recentTicketsBox, ticketPortfolio.RecentTickets);
            PopulateTickets(favoriteTicketsBox, ticketPortfolio.FavoriteTickets);
            PopulateTickets(assignedTicketsBox, ticketPortfolio.AssignedTickets);

            if (allocatableTime != null)
            {
                this.allocatableTime = allocatableTime.Value;

                var minuteFractions = (int)this.allocatableTime.TotalMinutes / 15;
                this.totalFractions = (int)realWorkTime.TotalMinutes / 15;
                this.timeTrackBar.Maximum = minuteFractions;

                this.hoursBox.Maximum = this.allocatableTime.Hours;
                this.maxHourMinMinuteIndex = GetMinuteIndex(this.allocatableTime.Minutes);
                this.minutesBox.BackColor = Color.White;
            }
            else
            {
                var totalMinutes = (timeRegistration.Hours * 60) + timeRegistration.Minutes;
                this.durationPanel.Enabled = false;
                this.hoursBox.Maximum = timeRegistration.Hours;
                this.timeTrackBar.Maximum = totalMinutes / 15;
                this.timeTrackBar.Value = this.timeTrackBar.Maximum;
            }

            this.Hours = timeRegistration.Hours;
            this.Minutes = timeRegistration.Minutes;
            this.SelectedTicket = timeRegistration.Ticket;
            this.Description = timeRegistration.Description;
            OKButtonEnableCheck();
        }

        private void PopulateTickets(ListBox targetListBox, IEnumerable<TicketInfo> tickets)
        {
            foreach (var ticket in tickets)
            {
                targetListBox.Items.Add(ticket.ToString());
            }
        }

        public string Description
        {
            get => descriptionBox.Text;
            set => descriptionBox.Text = value;
        }

        public TicketInfo SelectedTicket
        {
            get => selectedTicket;
            set
            {
                selectedTicket = value;
                if (value != null)
                {
                    OnExternalSelectedTicket();
                }
            }
        }

        public int Hours
        {
            get => (int)hoursBox.Value;
            set => hoursBox.Value = value;
        }

        public int Minutes
        {
            get => int.Parse(minutesBox.Text);
            set
            {
                var minuteIndex = GetMinuteIndex(value);
                prevMinuteSelectedIndex = minuteIndex;
                minutesBox.SelectedIndex = minuteIndex;
            }
        }

        private int GetMinuteIndex(int minutes)
        {
            var indexOffset = minutes / 15;
            return MAX_MINUTEBOX_INDEX - indexOffset;
        }

        private void OnExternalSelectedTicket()
        {
            if (HandleExternalTicketFocus(ticketPortfolio.FavoriteTickets, favoriteTicketsBox))
            {
                return;
            }

            if (HandleExternalTicketFocus(ticketPortfolio.RecentTickets, recentTicketsBox))
            {
                ticketGroupTabs.SelectedIndex = 1;
                return;
            }

            if (HandleExternalTicketFocus(ticketPortfolio.AssignedTickets, assignedTicketsBox))
            {
                ticketGroupTabs.SelectedIndex = 2;
                return;
            }

            ticketGroupTabs.SelectedIndex = 3;
            searchResults = new List<TicketInfo>() { selectedTicket };
            searchResultsBox.Items.Add(selectedTicket.ToString());
            searchResultsBox.SelectedIndex = 0;
        }

        private bool HandleExternalTicketFocus(IEnumerable<TicketInfo> ticketList, ListBox ticketsListBox)
        {
            var index = ticketList.IndexOf(selectedTicket);
            if (index == -1)
            {
                return false;
            }

            ticketsListBox.SelectedIndex = index;
            return true;
        }

        private void TicketRegistrationDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && searchBox.Focused)
            {
                searchResultsBox.Items.Clear();
                PerformTicketSearch();
            }
        }

        //Small hack since we have set an AcceptButton, which intercepts any "Enter" KeyDowns
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && searchBox.Focused)
            {
                searchResultsBox.Items.Clear();
                PerformTicketSearch();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PerformTicketSearch()
        {
            searchResults = ticketingSystem.SearchTickets(searchBox.Text);
            PopulateTickets(searchResultsBox, searchResults);
        }

        private void favoriteTicketsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleNewTicketSelection(favoriteTicketsBox, ticketPortfolio.FavoriteTickets);
        }

        private void recentTicketsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleNewTicketSelection(recentTicketsBox, ticketPortfolio.RecentTickets);
        }

        private void assignedTicketsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleNewTicketSelection(assignedTicketsBox, ticketPortfolio.AssignedTickets);
        }

        private void searchResultsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleNewTicketSelection(searchResultsBox, searchResults);
        }

        private void HandleNewTicketSelection(ListBox selectedBox, IList<TicketInfo> ticketList)
        {
            if (ignoreNewSelection)
            {
                return;
            }
            ignoreNewSelection = true;

            var selectedIndex = selectedBox.SelectedIndex;
            recentTicketsBox.SelectedIndex = -1;
            searchResultsBox.SelectedIndex = -1;
            favoriteTicketsBox.SelectedIndex = -1;
            assignedTicketsBox.SelectedIndex = -1;

            selectedTicket = selectedIndex != -1 ? ticketList[selectedIndex] : null;
            selectedBox.SelectedIndex = selectedIndex;
            OKButtonEnableCheck();

            ignoreNewSelection = false;
        }

        private void TicketsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var ticketsBox = (ListBox)sender;
            if (ticketsBox.IndexFromPoint(e.Location) != -1)
            {
                //Actually double clicked on an item :)
                OK_Button.PerformClick();
            }
        }

        private void descriptionBox_TextChanged(object sender, EventArgs e)
        {
            OKButtonEnableCheck();
        }

        private void hoursBox_ValueChanged(object sender, EventArgs e)
        {
            if (IsOnMaxHour() && minutesBox.SelectedIndex < maxHourMinMinuteIndex)
            {
                minutesBox.SelectedIndex = maxHourMinMinuteIndex;
            }
            OKButtonEnableCheck();
            OnTimeBoxUpdate();
        }

        private bool IsOnMaxHour()
        {
            return hoursBox.Value == allocatableTime.Hours;
        }

        private void minutesBox_SelectedItemChanged(object sender, EventArgs e)
        {
            var selectedIndex = minutesBox.SelectedIndex;
            if (selectedIndex == MAX_MINUTEBOX_INDEX && prevMinuteSelectedIndex == 0)
            {
                OnWrappedUp();
            }
            else if (selectedIndex == 0 && prevMinuteSelectedIndex == MAX_MINUTEBOX_INDEX)
            {
                OnWrappedDown();
            }
            else if (IsOnMaxHour() && selectedIndex < maxHourMinMinuteIndex)
            {
                //Our minutes are higher than the max, force it to the maximum allowed value
                prevMinuteSelectedIndex = maxHourMinMinuteIndex; //Prevents detecting a "WrapUp"
                minutesBox.SelectedIndex = maxHourMinMinuteIndex;
                return;
            }
            prevMinuteSelectedIndex = minutesBox.SelectedIndex; //NOTE: Can't use "selectedIndex", as the SelectedIndex can change due to the "OnWrappedUp/Down" handlers
            OKButtonEnableCheck();
            OnTimeBoxUpdate();
        }

        private void OnTimeBoxUpdate()
        {
            if (!durationPanel.Enabled)
            {
                percentageLabel.Text = "N/A";
                return;
            }

            var minuteFractions = ((int)hoursBox.Value * 4) + (this.Minutes / 15);
            percentageLabel.Text = (int)((minuteFractions / (float)totalFractions) * 100) + "%";
            timeTrackBar.Value = minuteFractions;
        }

        private void timeTrackBar_Scroll(object sender, EventArgs e)
        {
            var timeSpan = TimeSpan.FromMinutes(15 * timeTrackBar.Value);
            this.Minutes = timeSpan.Minutes;
            this.Hours = timeSpan.Hours;
        }

        private void OnWrappedUp()
        {
            if (hoursBox.Value == hoursBox.Maximum)
            {
                RestorePreviousMinuteSelectedIndex();
                return;
            }
            hoursBox.Value++;
        }

        private void OnWrappedDown()
        {
            if (hoursBox.Value == 0)
            {
                minutesBox.SelectedIndex = MAX_MINUTEBOX_INDEX;
                return;
            }
            hoursBox.Value--;
        }

        private void RestorePreviousMinuteSelectedIndex()
        {
            this.minutesBox.SelectedIndex = prevMinuteSelectedIndex;
        }

        private void OKButtonEnableCheck()
        {
            OK_Button.Enabled = CanEnableOKButton();
        }

        private bool CanEnableOKButton()
        {
            if (selectedTicket == null)
            {
                return false;
            }
            if (favoriteTicketsBox.SelectedIndex != -1 && string.IsNullOrEmpty(descriptionBox.Text))
            {
                //A description IS required for "favorite tickets"
                return false;
            }
            return !durationPanel.Enabled || hoursBox.Value != 0 || minutesBox.Text != "00";
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
