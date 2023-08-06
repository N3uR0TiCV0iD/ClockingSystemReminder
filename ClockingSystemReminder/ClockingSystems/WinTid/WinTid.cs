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
        const string MONTH_SCHEDULE_URL = BASE_URL + "WorkSchedule/GetCalendarInfoForActivePosition";

        WinTidUser user;

        public WinTid()
        {
            WebUtils.EnableTLS_12();
        }

        public override string GetWebLoginURL()
        {
            return BASE_URL;
        }

        public override bool Login(BasicCredentials credentials)
        {
            string payload = Utils.StringFormat(Resources.WinTidLoginPayload, credentials.Username, credentials.Password);

            HttpWebResponse webResponse = MakePOSTRequest(LOGIN_URL, payload);
            if (!IsResponseSuccess(webResponse))
            {
                return false;
            }
            this.user = GetWinTidUser();
            return true;
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
            HttpWebResponse webResponse = MakePayloadRequest(CLOCK_IN_URL);
            return IsResponseSuccess(webResponse);
        }

        public override bool ClockOut()
        {
            HttpWebResponse webResponse = MakePayloadRequest(CLOCK_OUT_URL);
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
            string payload = Resources.WinTidSchedulePayload
                                      .Replace("{0}", lastDay.ToString("yyyy-MM-dd"));

            MessageBox.Show(payload);
            return null; // ParseHolidays(Resources.scheduleResponse); //TMP

            /*
            HttpWebResponse webResponse = MakePOSTRequest(MONTH_SCHEDULE_URL, payload);
            if (IsResponseSuccess(webResponse, out string response))
            {
                return ParseHolidays(response);
            }
            return null;
            */
        }
        private DateTime[] ParseHolidays(string response)
        {
            List<DateTime> holidays = new List<DateTime>();
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
            bool retry = true;
            string errorReason = "UNKNOWN"; //IN THEORY it will never show "UNKNOWN"...
            string from = GetEndOfMonthString(today.Year, today.Month - 1);
            string to = GetEndOfMonthString(today.Year, today.Month);
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
            string payload = Utils.StringFormat(Resources.WinTidApprovePayload,
                                               from, to,
                                               user.EmployeeId, user.PositionId,
                                               user.Description, user.ActiveRole);

            HttpWebResponse webResponse = MakePOSTRequest(APPROVE_MONTH_URL, payload);
            return IsResponseSuccess(webResponse);
        }

        private bool ManualWeekApproval()
        {
            return false;
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
            return WebUtils.MakePOSTRequest(webRequest, requestContent, "application/json");
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
