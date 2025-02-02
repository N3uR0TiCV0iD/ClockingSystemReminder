using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data;
using ClockingSystemReminder.Data.Collaboration;
using ClockingSystemReminder.Extensions;
using ClockingSystemReminder.Helpers;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClockingSystemReminder.CollaborationSystems.MSTeams
{
    public class MSTeams : ICollaborationSystem
    {
        //TODO: Add token refresh
        const int NEXTURL_DELAY = 650;

        const string LOGIN_URL = "https://microsoft.com/devicelogin";
        const string MS_TEAMS_CLIENTID = "1fec8e78-bce4-4aaf-ab1b-5451cc387264";
        const string SKYPE_AUTH_URL = "https://authsvc.teams.microsoft.com/v1.0/authz";

        const string BASE_URL = "https://teams.microsoft.com/api/mt/part/emea-02/beta/";
        const string CALENDAR_URL = BASE_URL + "me/calendarEvents?";
        const string FETCH_USERS_URL = BASE_URL + "users/fetchShortProfile?isMailAddress=false&enableGuest=true&includeIBBarredUsers=true&skypeTeamsInfo=true";

        const string SKYPE_BASE_URL = "https://emea.ng.msg.teams.microsoft.com/";
        const string CHATS_URL = SKYPE_BASE_URL + "v1/users/ME/conversations";
        const string CALL_HISTORY_URL = SKYPE_BASE_URL + "v1/users/ME/conversations/48:calllogs/messages";

        public UserInfo MyUser { get; private set; }

        string skypeToken;
        string azureADToken;
        MSTeamsSettings settings;
        Dictionary<string, UserInfo> cachedUsers; //UserID (mri) => UserInfo

        public MSTeams()
        {
            this.cachedUsers = new Dictionary<string, UserInfo>();
        }

        public bool LoadSettings()
        {
            using (var registryKey = Utils.OpenSystemRegistryKey("MSTeams"))
            {
                this.settings = new MSTeamsSettings()
                {
                    TenantID = (string)registryKey.GetValue("TenantID")
                };
                return this.settings.IsValid();
            }
        }

        public void OpenSettings()
        {
            using (var settingsDialog = new MSTeamsSettingsDialog(this.settings))
            {
                if (settingsDialog.ShowDialog() == DialogResult.OK)
                {
                    settings.TenantID = settingsDialog.TenantID;
                    SaveSettings();
                }
            }
        }

        private void SaveSettings()
        {
            using (var registryKey = Utils.OpenSystemRegistryKey("MSTeams"))
            {
                registryKey.SetValue("TenantID", settings.TenantID);
            }
        }

        public bool IsConfigured()
        {
            return settings.IsValid();
        }

        public void Init()
        {
            if (this.MyUser != null)
            {
                return;
            }

            this.azureADToken = GetAzureADToken();
            this.skypeToken = GetSkypeToken();

            this.MyUser = BuildSelfUser();
            cachedUsers[this.MyUser.UserID] = this.MyUser;
        }

        private string GetAzureADToken()
        {
            var oauth2Client = BuildOAuth2Client();

            var scopes = new string[] { "https://api.spaces.skype.com/.default" };
            var result = oauth2Client.AcquireTokenWithDeviceCode(scopes, OnDeviceCodeFlowReady)
                                     .ExecuteAsync().Result;
            return result.AccessToken;
        }

        private IPublicClientApplication BuildOAuth2Client()
        {
            return PublicClientApplicationBuilder.Create(MS_TEAMS_CLIENTID)
                                                 .WithTenantId(settings.TenantID)
                                                 .Build();
        }

        private Task OnDeviceCodeFlowReady(DeviceCodeResult deviceCodeFlow)
        {
            Utils.OpenWebBrowser(LOGIN_URL);
            ClipboardHelper.SetText(deviceCodeFlow.UserCode);
            MessageBox.Show($"A web browser has been opened at https://login.microsoftonline.com/.\n\nPlease enter the code \"{deviceCodeFlow.UserCode}\" to authenticate.\n\n\nThe code has been copied to your clipboard for convenience.",
                            "AzureAD Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return Task.CompletedTask;
        }

        private string GetSkypeToken()
        {
            var webRequest = WebUtils.CreateRequestWithAuth(SKYPE_AUTH_URL, azureADToken);
            var webResponse = WebUtils.MakePOSTRequest(webRequest, string.Empty, string.Empty);

            var response = WebUtils.ReadResponse(webResponse);
            return response.ExtractBetween("skypeToken\":\"", '"'); //It's quite a big payload, let's just scan the string :)
        }

        private UserInfo BuildSelfUser()
        {
            const string selfChatID = "48:notes";
            var name = GetClaimFromToken(azureADToken, "name");
            var skypeID = GetClaimFromToken(skypeToken, "skypeid");
            var userID = $"8:{skypeID}";
            return new UserInfo(userID, selfChatID, name);
        }

        private string GetClaimFromToken(string accessToken, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(accessToken);

            var claims = jwtToken.Claims;
            foreach (var claim in claims)
            {
                if (claim.Type == claimType)
                {
                    return claim.Value;
                }
            }
            return null;
        }

        public IList<CalendarEvent> GetCalendarEvents(DateTime utcFrom, DateTime utcTo)
        {
            if (utcFrom > utcTo)
            {
                throw new ArgumentException("The start time cannot be later than the end time.");
            }

            var endDate = utcTo.ToString("O");
            var startDate = utcFrom.ToString("O");
            return GetCalendarEvents(startDate, endDate);
        }

        private IList<CalendarEvent> GetCalendarEvents(string startDate, string endDate)
        {
            var url = CALENDAR_URL + $"StartDate={startDate}&EndDate={endDate}";
            var webRequest = WebUtils.CreateRequestWithAuth(url, azureADToken);
            webRequest.Method = "GET";

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var response = WebUtils.ReadResponse(webResponse);
            var jsonResponse = JObject.Parse(response);
            return GetCalendarEvents(jsonResponse);
        }

        private IList<CalendarEvent> GetCalendarEvents(JObject jsonResponse)
        {
            var calendarEvents = new List<CalendarEvent>();

            var events = jsonResponse.Value<JArray>("value");
            foreach (var currEvent in events.Values<JObject>())
            {
                var isCancelled = currEvent.Value<bool>("isCancelled");
                if (isCancelled)
                {
                    //Well, next event I guess...
                    continue;
                }

                var subject = currEvent.Value<string>("subject");
                var chatID = GetCalendarEventChatID(currEvent);

                var endTime = currEvent.ValueAsUTCDateTime("endTime");
                var startTime = currEvent.ValueAsUTCDateTime("startTime");
                var period = new PeriodInfo(startTime, endTime);

                var calendarEvent = new CalendarEvent(subject, chatID, period);
                calendarEvents.Add(calendarEvent);
            }

            return calendarEvents;
        }

        private string GetCalendarEventChatID(JToken calendarEventJSON)
        {
            var skypeTeamsDataStr = calendarEventJSON.Value<string>("skypeTeamsData"); //NOTE: This is an encoded JSON string!
            if (skypeTeamsDataStr == null)
            {
                return null;
            }

            var skypeTeamsData = JObject.Parse(skypeTeamsDataStr);
            return skypeTeamsData.Value<string>("cid");
        }

        public CallRecord GetCallForCalendarEvent(CalendarEvent calendarEvent)
        {
            if (calendarEvent.ChatID == null)
            {
                return null;
            }

            var shouldStop = false;
            var currURL = GetChatURL(calendarEvent.ChatID);
            while (currURL != null)
            {
                var webResponse = MakeSkypeAPIRequest(currURL);
                var response = WebUtils.ReadResponse(webResponse);

                var jsonResponse = JObject.Parse(response);
                var call = FindCallForCalendarEvent(jsonResponse, calendarEvent, ref shouldStop);
                if (call != null)
                {
                    return call;
                }
                if (shouldStop)
                {
                    return null;
                }

                currURL = jsonResponse.Value<JObject>("_metadata").Value<string>("backwardLink");
                Thread.Sleep(NEXTURL_DELAY); //Don't spam their API :)
                //^ TODO: Replace with "await Task.Delay" when switching to C# Tasks
            }
            return null;
        }

        private CallRecord FindCallForCalendarEvent(JObject jsonResponse, CalendarEvent calendarEvent, ref bool shouldStop)
        {
            var eventPeriod = calendarEvent.Period;
            var messages = jsonResponse.Value<JArray>("messages");
            foreach (var message in messages.Values<JObject>())
            {
                var sendTime = message.ValueAsUTCDateTime("composetime");
                if (sendTime < eventPeriod.StartTime)
                {
                    //We have reached a message that happened before our event. Let's stop the search
                    shouldStop = true;
                    return null;
                }

                var messageType = message.Value<string>("messagetype");
                if (messageType != "Event/Call")
                {
                    //Not a call, next message please
                    continue;
                }

                var callRecord = BuildCallRecordFromCallMessage(calendarEvent, message, sendTime);
                if (callRecord.Period.StartTime > eventPeriod.EndTime)
                {
                    //EDGE CASE: The call started outside the event, let's try and look for a different message
                    continue;
                }
                return callRecord;
            }
            //The call was not found in the fetched messages
            return null;
        }

        private CallRecord BuildCallRecordFromCallMessage(CalendarEvent calendarEvent, JObject callMessage, DateTime messageSendTime)
        {
            const string endedTag = "<ended/>";
            var content = callMessage.Value<string>("content"); //NOTE: This is an XML string!
            if (content.StartsWith(endedTag))
            {
                content = content.Substring(endedTag.Length);
            }

            const string expectedEndTag = "</partlist>";
            if (!content.EndsWith(expectedEndTag))
            {
                var substringEnd = content.LastIndexOfAfter(expectedEndTag);
                content = content.Substring(0, substringEnd);
            }

            var callData = XDocument.Parse(content);
            var participantsList = callData.Descendants("partlist");
            var participants = ReadParticipants(participantsList, out int duration);

            var startTime = messageSendTime.AddSeconds(-duration);
            var period = new PeriodInfo(startTime, messageSendTime);

            return new CallRecord(calendarEvent.ChatID, period, participants);
        }

        private HashSet<UserInfo> ReadParticipants(IEnumerable<XElement> participantsList, out int duration)
        {
            var participantIDs = new HashSet<string>();
            var participants = participantsList.Elements("part");
            var firstParticipant = participants.FirstOrDefault();
            if (firstParticipant == null)
            {
                duration = 0;
                return new HashSet<UserInfo>();
            }

            duration = int.Parse(firstParticipant.Element("duration").Value); //NOTE: All participants have the same value for "duration"
            foreach (var participant in participants)
            {
                var participantID = participant.Element("name").Value;
                participantIDs.Add(participantID);
            }
            return ResolveUsers(participantIDs);
        }

        public IEnumerable<CallRecord> GetCallHistory()
        {
            var currURL = CALL_HISTORY_URL;
            while (currURL != null)
            {
                var webResponse = MakeSkypeAPIRequest(currURL);
                var response = WebUtils.ReadResponse(webResponse);

                var jsonResponse = JObject.Parse(response);
                var callMessages = jsonResponse.Value<JArray>("messages");
                foreach (var call in callMessages.Values<JObject>())
                {
                    var callLogData = call.Value<JObject>("properties").Value<string>("call-log"); //NOTE: This is an encoded JSON string!
                    var callLog = JObject.Parse(callLogData);

                    var connectTime = callLog.ValueAsNullableUTCDateTime("connectTime"); //Basically when YOU joined
                    if (connectTime == null)
                    {
                        //Seems like we missed/rejected this call
                        continue;
                    }

                    var callRecord = BuildCallRecord(callLog, connectTime.Value);
                    yield return callRecord;
                }

                currURL = jsonResponse.Value<JObject>("_metadata").Value<string>("backwardLink");
                Thread.Sleep(NEXTURL_DELAY); //Don't spam their API :)
                //^ TODO: Replace with "await Task.Delay" when switching to C# Tasks
            }
        }

        private CallRecord BuildCallRecord(JObject callLog, DateTime connectTime)
        {
            var participantIDs = GetParticipantIDs(callLog);
            var participants = ResolveUsers(participantIDs);
            var chatID = GetCallChatID(callLog, participants);

            var endTime = callLog.ValueAsUTCDateTime("endTime");
            var period = new PeriodInfo(connectTime, endTime);

            return new CallRecord(chatID, period, participants);
        }

        private HashSet<string> GetParticipantIDs(JObject callLog)
        {
            var participants = callLog.Value<JArray>("participants");
            if (participants == null)
            {
                //1 on 1 call
                var originator = callLog.Value<string>("originator");
                var target = callLog.Value<string>("target");
                return new HashSet<string> { originator, target };
            }
            var validParticipants = participants.Values<string>()
                                                .Where(p => p.StartsWith("8:"));
            var participantIDs = new HashSet<string>(validParticipants);
            return participantIDs;
        }

        private HashSet<UserInfo> ResolveUsers(HashSet<string> userIDs)
        {
            var users = new HashSet<UserInfo>();
            var missingUserIDs = new List<string>();
            foreach (var userID in userIDs)
            {
                if (cachedUsers.TryGetValue(userID, out UserInfo userInfo))
                {
                    users.Add(userInfo);
                }
                else
                {
                    missingUserIDs.Add(userID);
                }
            }

            if (missingUserIDs.Count != 0)
            {
                var fetchedUsers = FetchUsers(missingUserIDs);
                foreach (var user in fetchedUsers)
                {
                    cachedUsers.Add(user.UserID, user);
                    users.Add(user);
                }
            }
            return users;
        }

        private IList<UserInfo> FetchUsers(IEnumerable<string> userIDs)
        {
            var requestContent = JsonConvert.SerializeObject(userIDs);
            var webRequest = WebUtils.CreateRequestWithAuth(FETCH_USERS_URL, azureADToken);

            var webResponse = WebUtils.MakeJsonPOSTRequest(webRequest, requestContent);
            var response = WebUtils.ReadResponse(webResponse);
            var jsonResponse = JObject.Parse(response);
            return ReadUsers(jsonResponse);
        }

        private IList<UserInfo> ReadUsers(JObject jsonResponse)
        {
            var users = new List<UserInfo>();
            var myUserGUID = GetUserGUID(this.MyUser.UserID);

            var values = jsonResponse.Value<JArray>("value");
            foreach (var user in values.Values<JObject>())
            {
                var userID = user.Value<string>("mri"); //Microsoft Resource Identifier?
                if (!userID.StartsWith("8:orgid:"))
                {
                    continue;
                }

                var givenName = user.Value<string>("givenName");
                var surname = user.Value<string>("surname");

                var userGUID = GetUserGUID(userID);
                var chatID = BuildChatID(myUserGUID, userGUID);
                var fullName = $"{givenName} {surname}";

                var userInfo = new UserInfo(userID, chatID, fullName);
                users.Add(userInfo);
            }
            return users;
        }

        private Guid GetUserGUID(string userID)
        {
            var guid = userID.Substring(8); //Remove "8:orgid:"
            return Guid.Parse(guid);
        }

        private string BuildChatID(Guid id1, Guid id2)
        {
            //This seems to be the case from observation...
            if (id1.CompareTo(id2) < 0)
            {
                return $"19:{id1}_{id2}@unq.gbl.spaces";
            }
            return $"19:{id2}_{id1}@unq.gbl.spaces";
        }

        private string GetCallChatID(JObject callLog, HashSet<UserInfo> participants)
        {
            if (participants.Count == 2)
            {
                return participants.First(p => p != this.MyUser).ChatID;
            }
            return callLog.Value<string>("threadId");
        }

        //TODO: Switch to "IAsyncEnumerable" when switching to C# Tasks
        public IEnumerable<ChatRecord> GetChatHistory(string chatID)
        {
            var currURL = GetChatURL(chatID);
            while (currURL != null)
            {
                var webResponse = MakeSkypeAPIRequest(currURL);
                var response = WebUtils.ReadResponse(webResponse);

                var jsonResponse = JObject.Parse(response);
                var messages = jsonResponse.Value<JArray>("messages");
                foreach (var message in messages.Values<JObject>())
                {
                    var chatRecord = BuildChatRecord(message);
                    yield return chatRecord;
                }

                currURL = jsonResponse.Value<JObject>("_metadata").Value<string>("backwardLink");
                Thread.Sleep(NEXTURL_DELAY); //Don't spam their API :)
                //^ TODO: Replace with "await Task.Delay" when switching to C# Tasks
            }
        }

        private ChatRecord BuildChatRecord(JObject chatMessage)
        {
            return new ChatRecord(
                chatMessage.Value<string>("imdisplayname"),
                chatMessage.Value<string>("content"),
                chatMessage.ValueAsUTCDateTime("composetime")
            );
        }

        private HttpWebResponse MakeSkypeAPIRequest(string url)
        {
            var webRequest = WebUtils.CreateNormalRequest(url);
            webRequest.Method = "GET";
            webRequest.Headers.Add("authentication", $"skypetoken={this.skypeToken}"); //Huh, notice how it is "authentication" and not "Authorization"
            return (HttpWebResponse)webRequest.GetResponse();
        }

        private string GetChatURL(string chatID)
        {
            return $"{CHATS_URL}/{chatID}/messages";
        }
    }
}
