using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data.Ticketing;
using ClockingSystemReminder.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClockingSystemReminder.TicketingSystems.Jira
{
    public class Jira : ITicketingSystem
    {
        const string MYSELF_URI = "rest/api/3/myself";
        const string QUERY_URI = "rest/api/3/search";
        const string SEARCH_URI = "rest/api/3/issue/picker?currentJQL=project%20in%20projectsWhereUserHasPermission(%22Work%20on%20issues%22)%20order%20by%20lastViewed%20DESC&showSubTasks=true&showSubTaskParent=true";

        const string TEMPO_URL = "https://api.tempo.io/";
        const string WORKLOG_URL = TEMPO_URL + "4/worklogs";

        string accountID;
        JiraSettings settings;

        public bool LoadSettings()
        {
            using (var registryKey = Utils.OpenSystemRegistryKey("Jira"))
            {
                this.settings = new JiraSettings()
                {
                    URL = (string)registryKey.GetValue("URL"),
                    Email = (string)registryKey.GetValue("Email"),
                    JiraToken = registryKey.GetEncryptedValue("JiraToken", Program.LocalEncrypter),
                    TempoToken = registryKey.GetEncryptedValue("TempoToken", Program.LocalEncrypter)
                };
                return this.settings.IsValid();
            }
        }

        public void OpenSettings()
        {
            using (var settingsDialog = new JiraSettingsDialog(this.settings))
            {
                if (settingsDialog.ShowDialog() == DialogResult.OK)
                {
                    this.settings = settingsDialog.NewSettings;
                    SaveSettings();
                }
            }
        }

        private void SaveSettings()
        {
            using (var registryKey = Utils.OpenSystemRegistryKey("Jira"))
            {
                registryKey.SetValue("URL", settings.URL);
                registryKey.SetValue("Email", settings.Email);
                registryKey.SetEncryptedValue("JiraToken", settings.JiraToken, Program.LocalEncrypter);
                registryKey.SetEncryptedValue("TempoToken", settings.TempoToken, Program.LocalEncrypter);
            }
        }

        public bool IsConfigured()
        {
            return settings.IsValid();
        }

        public void Init()
        {
            this.accountID = GetAccountID();
        }

        private string GetAccountID()
        {
            //Could be cached in theory...
            var webResponse = MakeJiraGETRequest(MYSELF_URI);
            var response = WebUtils.ReadResponse(webResponse);
            return response.ExtractBetween("accountId=", '"');
        }

        public IList<TicketInfo> GetFavoriteTickets()
        {
            var favoriteTickets = FetchFavoriteTickets();
            var stringified = favoriteTickets.Select(ft => $"\"{ft}\"");

            var ticketKeys = string.Join(", ", stringified);
            var jql = $"issue in ({ticketKeys}) order by lastViewed DESC";
            return QueryTickets(jql);
        }

        private string[] FetchFavoriteTickets()
        {
            //TODO: Fetch from official Tempo API (not supported atm)
            return new string[] { "TT-53", "TT-56", "TT-123", "TT-140", "TT-145", "TT-122", "TT-125", "TT-128" };
        }

        public IList<TicketInfo> GetAssignedTickets()
        {
            var jql = $"assignee = {accountID} AND statusCategory != Done";
            return QueryTickets(jql);
        }

        private IList<TicketInfo> QueryTickets(string jql)
        {
            var baseURL = settings.URL + QUERY_URI;
            var escapedJQL = Uri.EscapeDataString(jql);
            var url = $"{baseURL}?jql={escapedJQL}&fields=summary,customfield_10008";

            var webRequest = WebUtils.CreateRequestWithAuth(url, settings.Email, settings.JiraToken);
            webRequest.Headers.Add("X-Atlassian-Token", "no-check"); //Bypass CORS
            webRequest.Method = "GET";

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var response = WebUtils.ReadResponse(webResponse);
            var jsonResponse = JObject.Parse(response);
            return ReadQueryResults(jsonResponse);
        }

        private IList<TicketInfo> ReadQueryResults(JObject jsonResponse)
        {
            var results = new List<TicketInfo>();
            var issues = jsonResponse.Value<JArray>("issues");
            foreach (var issue in issues.Values<JObject>())
            {
                var ticketInfo = ReadTicketInfo(issue);
                results.Add(ticketInfo);
            }
            return results;
        }

        private TicketInfo ReadTicketInfo(JObject issue)
        {
            var summary = ReadTicketSummary(issue);
            return new TicketInfo(
                issue.Value<int>("id"),
                issue.Value<string>("key"),
                summary
            );
        }

        private string ReadTicketSummary(JObject issue)
        {
            var summary = issue.Value<string>("summary");
            if (string.IsNullOrEmpty(summary))
            {
                var fields = issue.Value<JObject>("fields");
                summary = fields.Value<string>("summary");
            }
            return summary;
        }

        public IList<TicketInfo> SearchTickets(string query)
        {
            var refinedQuery = RefineQuery(query);
            var uri = SEARCH_URI + "&query=" + refinedQuery;

            var webResponse = MakeJiraGETRequest(uri);
            var response = WebUtils.ReadResponse(webResponse);
            var jsonResponse = JObject.Parse(response);

            return ReadSearchResults(jsonResponse);
        }

        private string RefineQuery(string query)
        {
            query = query.Trim();
            if (query.StartsWith(settings.URL))
            {
                var substringStart = query.LastIndexOf('/') + 1;
                var ticketNumber = query.Substring(substringStart);
                return ticketNumber;
            }
            return Uri.EscapeDataString(query);
        }

        public IList<TicketInfo> ReadSearchResults(JObject jsonResponse)
        {
            var results = new List<TicketInfo>();
            var addedTickets = new HashSet<int>();
            var sections = jsonResponse.Value<JArray>("sections");
            foreach (var section in sections.Values<JObject>())
            {
                var issues = section.Value<JArray>("issues");
                foreach (var issue in issues.Values<JObject>())
                {
                    var ticketInfo = ReadTicketInfo(issue);
                    if (!addedTickets.Contains(ticketInfo.ID))
                    {
                        results.Add(ticketInfo);
                        addedTickets.Add(ticketInfo.ID);
                    }
                }
            }
            return results;
        }

        private HttpWebResponse MakeJiraGETRequest(string uri)
        {
            var url = settings.URL + uri;
            var webRequest = WebUtils.CreateRequestWithAuth(url, settings.Email, settings.JiraToken);
            webRequest.Method = "GET";

            return (HttpWebResponse)webRequest.GetResponse();
        }

        public bool RegisterHours(DateTime workDate, TimeRegistration timeRegistration)
        {
            var secondsWorked = (timeRegistration.Hours * 3600) + (timeRegistration.Minutes * 60);
            var payload = new TempoPayload(accountID, timeRegistration.Ticket.ID, workDate, secondsWorked, timeRegistration.Description);
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var webRequest = WebUtils.CreateRequestWithAuth(WORKLOG_URL, settings.TempoToken);
            var webResponse = WebUtils.MakeJsonPOSTRequest(webRequest, jsonPayload);
            return webResponse.StatusCode == HttpStatusCode.OK;
        }
    }
}
