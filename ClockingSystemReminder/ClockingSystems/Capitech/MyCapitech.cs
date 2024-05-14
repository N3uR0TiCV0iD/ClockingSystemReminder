using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ClockingSystemReminder.Abstractions;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data;
using Microsoft.Win32;

namespace ClockingSystemReminder.ClockingSystems.Capitech
{
    public class MyCapitech : ClockingSystem
    {
        const string WEB_URL = "https://flow.capitech.no/gothia/apps/MinCapitech/index.htm?v=15.9.0.346";
        const string BASE_URL = "https://flow.capitech.no/gothia/api/public/v1/Webtid/";
        const string AVAILABLE_CLIENTS_URL = BASE_URL + "getAvaliableCapitechClients";
        const string CLOCK_IN_URL = BASE_URL + "createDefaultTimeTransactionIn";
        const string CLOCK_OUT_URL = BASE_URL + "createTimeTransactionOut";
        const string LOGIN_URL = BASE_URL + "authorize";
        const string LOGOUT_URL = BASE_URL + "signOut";

        string accessToken;
        List<CapitechLoginClient> loginClients;

        //Lazy getter
        private List<CapitechLoginClient> LoginClients
        {
            get
            {
                if (loginClients == null)
                {
                    loginClients = GetLoginClients();
                }
                return loginClients;
            }
        }

        public override void LoadSettings(RegistryKey systemRegistryKey)
        {
            throw new NotImplementedException();
        }

        private List<CapitechLoginClient> GetLoginClients()
        {
            var webResponse = MakePOSTRequest(AVAILABLE_CLIENTS_URL, null);
            using (var responseReader = new StreamReader(webResponse.GetResponseStream()))
            {
                var response = responseReader.ReadToEnd();
                if (!IsResponseSuccess(response, out int successEndCharIndex))
                {
                    return null;
                }
                return ParseClients(response, successEndCharIndex);
            }
        }

        private List<CapitechLoginClient> ParseClients(string response, int successEndCharIndex)
        {
            //Hey! So this code was written before I really had a need for a JSON library.
            //It's pretty messy, but I no longer have access to the response payload to refactor it...
            var clients = new List<CapitechLoginClient>();
            var currCharIndex = successEndCharIndex;
            while (currCharIndex < response.Length)
            {
                int clientNameEndIndex = ExtractJSONString(response, "clientName", out string clientName, currCharIndex);
                if (clientNameEndIndex != response.Length)
                {
                    int clientIDEndIndex = ExtractJSONInteger(response, "clientId", out int clientID, currCharIndex);
                    if (clientIDEndIndex != response.Length)
                    {
                        clients.Add(new CapitechLoginClient(clientID, clientName));
                    }
                    currCharIndex = Math.Max(clientNameEndIndex, clientIDEndIndex);
                }
                else
                {
                    currCharIndex = response.Length; //Break the while loop
                }
            }
            return clients;
        }

        public override string GetWebLoginURL()
        {
            return WEB_URL;
        }

        public override BasicCredentials GetStoredCredentials(RegistryKey systemRegistryKey)
        {
            object clientID = systemRegistryKey.GetValue("ClientID");
            if (clientID != null && TryGetStoredCredentials(systemRegistryKey, out string username, out string password))
            {
                return new MyCapitechCredentials((int)clientID, username, password);
            }
            return null;
        }

        public override AbstractLoginDialog CreateLoginDialog()
        {
            return new MyCapitechSettingsDialog(this.LoginClients);
        }

        public override BasicCredentials GetCredentialsFromLoginDialog(AbstractLoginDialog loginDialog)
        {
            var customLoginDialog = (MyCapitechSettingsDialog)loginDialog;
            return new MyCapitechCredentials(customLoginDialog.ClientID, loginDialog.Username, loginDialog.Password);
        }

        public override bool Login(BasicCredentials credentials)
        {
            var clientID = GetClientID(credentials);
            var webResponse = MakePOSTRequest(LOGIN_URL, $"clientId={clientID}&username={credentials.Username}&password={credentials.Password}");
            using (var responseReader = new StreamReader(webResponse.GetResponseStream()))
            {
                var response = responseReader.ReadToEnd();
                if (IsResponseSuccess(response, out int successEndIndex))
                {
                    ExtractJSONString(response, "accessToken", out accessToken, successEndIndex);
                    return true;
                }
            }
            accessToken = null;
            return false;
        }

        private int GetClientID(BasicCredentials credentials)
        {
            return ((MyCapitechCredentials)credentials).ClientID;
        }

        public override void SaveCredentials(BasicCredentials credentials, RegistryKey systemRegistryKey)
        {
            base.SaveCredentials(credentials, systemRegistryKey);
            systemRegistryKey.SetValue("ClientID", GetClientID(credentials), RegistryValueKind.DWord);
        }

        public override void DropCredentials(RegistryKey systemRegistryKey)
        {
            base.DropCredentials(systemRegistryKey);
            systemRegistryKey.DeleteValue("ClientID");
        }

        public override bool ClockIn()
        {
            return ClockAction(CLOCK_IN_URL, accessToken);
        }

        public override bool ClockOut()
        {
            return ClockAction(CLOCK_OUT_URL, accessToken);
        }

        private bool ClockAction(string url, string accessToken)
        {
            var webResponse = MakePOSTRequest(url, "accessToken=" + accessToken);
            return IsResponseSuccess(WebUtils.ReadResponse(webResponse));
        }

        private bool IsResponseSuccess(string response)
        {
            return IsResponseSuccess(response, out _);
        }

        private bool IsResponseSuccess(string response, out int successEndIndex)
        {
            const string SUCCESS_STRING = "{\"success\":true";
            if (response.StartsWith(SUCCESS_STRING))
            {
                successEndIndex = SUCCESS_STRING.Length;
                return true;
            }
            successEndIndex = response.Length;
            return false;
        }

        private int ExtractJSONString(string text, string key, out string result, int startIndex)
        {
            //Hey! So this code was written before I really had a need for a JSON library.
            //It's pretty messy, but I no longer have access to the response payload to refactor it...
            string searchString = "\"" + key + "\":\"";
            int keyStartIndex = text.IndexOf(searchString, startIndex);
            if (keyStartIndex != -1)
            {
                int substringStart = keyStartIndex + searchString.Length;
                int currCharIndex = substringStart;
                char lastChar = '\0';
                while (currCharIndex < text.Length)
                {
                    char currChar = text[currCharIndex];
                    if (currChar == '"' && lastChar != '\\')
                    {
                        result = text.Substring(substringStart, currCharIndex - substringStart);
                        return currCharIndex;
                    }
                    else
                    {
                        lastChar = currChar;
                        currCharIndex++;
                    }
                }
            }
            result = null;
            return text.Length;
        }

        private int ExtractJSONInteger(string text, string key, out int result, int startIndex)
        {
            //Hey! So this code was written before I really had a need for a JSON library.
            //It's pretty messy, but I no longer have access to the response payload to refactor it...
            string searchString = "\"" + key + "\":";
            int keyStartIndex = text.IndexOf(key, startIndex);
            if (keyStartIndex != -1)
            {
                int currCharIndex = keyStartIndex + searchString.Length - 1;
                bool negative = text[currCharIndex] == '-';
                char currChar;
                if (negative)
                {
                    currCharIndex++;
                }
                result = 0;
                currChar = text[currCharIndex++];
                while (char.IsDigit(currChar))
                {
                    result = (result * 10) + (currChar - '0');
                    currChar = text[currCharIndex++];
                }
                return currCharIndex;
            }
            result = 0;
            return text.Length;
        }

        private HttpWebResponse MakePOSTRequest(string url, string requestContent)
        {
            HttpWebRequest webRequest = WebUtils.CreateNormalRequest(url);
            AddRequiredHttpHeaders(webRequest);
            return WebUtils.MakePOSTRequest(webRequest, requestContent);
        }

        private void AddRequiredHttpHeaders(HttpWebRequest webRequest)
        {
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36";
            webRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            webRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
        }

        public override void OpenSettings()
        {
        }

        public override bool LogOut()
        {
            //HTTP GET => LOGOUT_URL
            return true;
        }
    }
}
