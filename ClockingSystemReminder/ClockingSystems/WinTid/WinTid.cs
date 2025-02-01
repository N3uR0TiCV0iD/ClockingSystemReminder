using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data;
using ClockingSystemReminder.Extensions;
using ClockingSystemReminder.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClockingSystemReminder.ClockingSystems.WinTid
{
    public class WinTid : ClockingSystem
    {
        const string BASE_URL = "https://no-1034.wintid.no/";
        const string LOGIN_URL = BASE_URL + "Account/LogOn";
        const string LOGOUT_URL = BASE_URL + "Account/LogOff";
        const string CLOCK_IN_URL = BASE_URL + "Registration/RegisterIn";
        const string CLOCK_OUT_URL = BASE_URL + "Registration/RegisterOut";
        const string APPROVE_MONTH_URL = BASE_URL + "Overview/SetPeriodApproval";
        const string GET_REGISTRATIONS_URL = BASE_URL + "Maintenance/GetDayModel";
        const string MANUAL_REGISTRATION_URL = BASE_URL + "Maintenance/SaveRegistrations";
        const string MONTH_SCHEDULE_URL = BASE_URL + "WorkSchedule/GetCalendarInfoForActivePosition";

        WinTidUser user;
        string csrfToken; //"Cross-Site Request Forgery" protection token received from the server

        public WinTid()
        {
            WebUtils.EnableTLS_12();
        }

        public override string GetWebLoginURL()
        {
            return BASE_URL;
        }

        public bool Login()
        {
            using (var appRegistryKey = OpenRegistryKey())
            {
                var credentials = GetStoredCredentials(appRegistryKey);
                return Login(credentials);
            }
        }

        public override bool Login(BasicCredentials credentials)
        {
            if (string.IsNullOrEmpty(csrfToken))
            {
                RefreshCSRFToken();
            }

            var payload = Utils.StringFormat(Resources.WinTidLoginPayload, credentials.Username, credentials.Password);
            var webResponse = MakePOSTRequest(LOGIN_URL, payload);
            if (!IsResponseSuccess(webResponse))
            {
                return false;
            }
            this.user = GetWinTidUser();
            return true;
        }

        private void RefreshCSRFToken()
        {
            var webResponse = WebUtils.MakeGETRequestWithCookies(BASE_URL);
            var response = WebUtils.ReadResponse(webResponse);

            //Scan the HTML
            csrfToken = response.ExtractBetween("var REQUEST_VERIFICATION_TOKEN = \"", '"');
        }

        private WinTidUser GetWinTidUser()
        {
            var webResponse = WebUtils.MakeGETRequestWithCookies(BASE_URL);
            var response = WebUtils.ReadResponse(webResponse);

            //Scan the HTML
            var userData = response.ExtractBetween("var AUTHENTICATED_IDENTITY = ", '}', true);
            return JsonConvert.DeserializeObject<WinTidUser>(userData);
        }

        public override bool ClockIn()
        {
            var webResponse = MakePayloadRequest(CLOCK_IN_URL);
            return IsResponseSuccess(webResponse);
        }

        public override bool ClockOut()
        {
            var webResponse = MakePayloadRequest(CLOCK_OUT_URL);
            return IsResponseSuccess(webResponse);
        }

        public override bool OnPostClockIn()
        {
            var today = DateTime.Today;
            var weekNumber = Utils.GetWeekNumber(today);
            using (var registryKey = base.OpenRegistryKey())
            {
                var lastWeekNumber = (int)registryKey.GetValue("LastWeekNumber", -1);
                if (weekNumber != lastWeekNumber)
                {
                    bool success = ApproveWeekProcess(today);
                    if (success)
                    {
                        registryKey.SetValue("LastWeekNumber", weekNumber, RegistryValueKind.DWord);
                    }
                    return success;
                }
            }
            return true;
        }

        private DateTime[] GetHolidays(DateTime lastDay)
        {
            var payload = Resources.WinTidSchedulePayload
                                   .Replace("{0}", lastDay.ToString("yyyy-MM-dd"));

            MessageBox.Show(payload);
            return null; // ParseHolidays(Resources.scheduleResponse); //TMP

            /*
            var webResponse = MakePOSTRequest(MONTH_SCHEDULE_URL, payload);
            if (IsResponseSuccess(webResponse, out string response))
            {
                return ParseHolidays(response);
            }
            return null;
            */
        }
        private DateTime[] ParseHolidays(string response)
        {
            var holidays = new List<DateTime>();
            //TODO: Implement parse algorithm (Check scheduleResponse.txt)
            return holidays.ToArray();
        }

        private bool ApproveWeekProcess(DateTime today)
        {
            if (MessageBox.Show("Looks like it's time to approve last week!\n\nWould you like to automatically approve last week?",
                                "Auto approve week?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return AutoApproveWeek(today);
            }
            ManualWeekApproval();
            return true;
        }

        private bool AutoApproveWeek(DateTime today)
        {
            var retry = true;
            var errorReason = "UNKNOWN"; //IN THEORY it will never show "UNKNOWN"...
            var from = GetEndOfMonthString(today.Year, today.Month - 1);
            var to = GetEndOfMonthString(today.Year, today.Month);
            while (retry)
            {
                try
                {
                    if (!AutoApproveWeek(from, to))
                    {
                        errorReason = "Server refused week-approval request";
                        //TODO: REFACTOR ME!!!
                        MessageBox.Show($"There was an error while trying to automatically approve this week!" +
                                        $"\n\nReason: {errorReason}", $"Auto week-approval failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Auto week approval was successful!", "Successful week-approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
                catch (WebException ex)
                {
                    if (!WebUtils.ShowNetworkRetryMessage(ex))
                    {
                        errorReason = "Network error";
                        retry = false;
                    }
                }
            }
            MessageBox.Show($"There was an error while trying to automatically approve this week!\nBecause of this, you will need to manually approve it" +
                            $"\n\nReason: {errorReason}", $"Auto week-approval failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ManualWeekApproval();
        }

        private string GetEndOfMonthString(int year, int month)
        {
            if (month == 0)
            {
                month = 12;
                year--;
            }
            return $"{year}-{month}-{DateTime.DaysInMonth(year, month)}";
        }

        private bool AutoApproveWeek(string from, string to)
        {
            var payload = Utils.StringFormat(Resources.WinTidApprovePayload,
                                             from, to,
                                             user.EmployeeId, user.PositionId,
                                             user.Description, user.ActiveRole);

            var webResponse = MakePOSTRequest(APPROVE_MONTH_URL, payload);
            return IsResponseSuccess(webResponse);
        }

        private bool ManualWeekApproval()
        {
            return false;
        }

        public override TimeSpan GetTimeWorked(DateTime day)
        {
            if (user == null)
            {
                Login();
            }

            var payload = Utils.StringFormat(Resources.WinTidRegistrationsPayload,
                                             day.AddDays(-1).ToString("yyyy-MM-dd"),
                                             user.EmployeeId, user.PositionId,
                                             user.Description, user.ActiveRole);

            var webResponse = MakePOSTRequest(GET_REGISTRATIONS_URL, payload);
            if (!IsResponseSuccess(webResponse, out string response))
            {
                return TimeSpan.Zero;
            }

            var workSessions = ParseWorkSessions(response);
            var totalWorkTime = Utils.SumTimeSpans(workSessions);
            return totalWorkTime;
        }

        private List<TimeSpan> ParseWorkSessions(string response)
        {
            var jsonResponse = JObject.Parse(response);
            var data = jsonResponse.Value<JObject>("Data");
            var dayData = data.Value<JObject>("ChangeDayViewModel");

            var timestamps = new List<DateTime>();
            var registrationData = dayData.Value<JArray>("VisDag");
            foreach (var registration in registrationData.Values<JObject>())
            {
                var flag = registration.Value<int>("Flag");
                if (flag != 0)
                {
                    continue;
                }

                var timestamp = registration.Value<DateTime>("DateTime");
                timestamps.Add(timestamp);
            }

            var workSessions = new List<TimeSpan>();
            for (int index = 1; index < timestamps.Count; index += 2)
            {
                var to = timestamps[index];
                var from = timestamps[index - 1];
                var workSession = to - from;
                workSessions.Add(workSession);
            }
            return workSessions;
        }

        private HttpWebResponse MakePayloadRequest(string url)
        {
            var payload = Utils.StringFormat(Resources.WinTidPayload,
                                             user.EmployeeId, user.PositionId,
                                             user.Description, user.ActiveRole);
            return MakePOSTRequest(url, payload);
        }

        private HttpWebResponse MakePOSTRequest(string url, string requestContent)
        {
            var webRequest = WebUtils.CreateRequestWithCookies(url);
            AddRequiredHeaders(webRequest.Headers);
            return WebUtils.MakePOSTRequest(webRequest, requestContent, "application/json");
        }

        private void AddRequiredHeaders(WebHeaderCollection headers)
        {
            headers.Add("RequestVerificationToken", csrfToken);
        }

        private bool IsResponseSuccess(HttpWebResponse webResponse)
        {
            return IsResponseSuccess(webResponse, out _);
        }

        private bool IsResponseSuccess(HttpWebResponse webResponse, out string response)
        {
            const string SUCCESS_STRING = "\"ResponseType\":\"Success\"}";
            response = WebUtils.ReadResponse(webResponse);
            return response.EndsWith(SUCCESS_STRING);
        }

        public override bool LogOut()
        {
            //HTTP GET => LOGOUT_URL
            return true;
        }
    }
}
