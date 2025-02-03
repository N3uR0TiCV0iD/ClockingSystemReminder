using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClockingSystemReminder.Extensions;
using SSKeyValuePair = System.Collections.Generic.KeyValuePair<string, string>;

namespace ClockingSystemReminder
{
    public static class WebUtils
    {
        //TODO: Modernize and use Tasks

        static CookieContainer cookies;

        public static void EnableTLS_12()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)0xC00;
        }

        public static void DisableSSLValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback += SSLValidationBypass;
        }

        private static bool SSLValidationBypass(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static bool ShowNetworkRetryMessage(WebException exception)
        {
            var message = $"A network error has occured!\n\nDescription:\n{exception.Message}\n\nPlease make sure you are connected to the internet before retrying";
            return Utils.ShowRetryMessage(message, "Network error");
        }

        public static string DownloadString(string url)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }

        public static HttpWebRequest CreateNormalRequest(string url)
        {
            return (HttpWebRequest)WebRequest.Create(url);
        }

        public static HttpWebRequest CreateRequestWithAuth(string url, string username, string password)
        {
            var webRequest = CreateNormalRequest(url);
            AddAuthHeader(webRequest.Headers, username, password);
            return webRequest;
        }

        public static HttpWebRequest CreateRequestWithAuth(string url, string token)
        {
            var webRequest = CreateNormalRequest(url);
            AddAuthHeader(webRequest.Headers, token);
            return webRequest;
        }

        public static void AddAuthHeader(WebHeaderCollection headers, string username, string password)
        {
            headers.Add(HttpRequestHeader.Authorization, "Basic " + ToBase64String(username + ":" + password));
        }

        public static void AddAuthHeader(WebHeaderCollection headers, string token)
        {
            headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
        }

        private static string ToBase64String(string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static HttpWebRequest CreateRequestWithCookies(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cookies == null)
            {
                cookies = new CookieContainer();
            }
            webRequest.CookieContainer = cookies;
            return webRequest;
        }

        public static HttpWebResponse MakeGETRequestWithCookies(string url)
        {
            var webRequest = CreateRequestWithCookies(url);
            webRequest.Method = "GET";
            return (HttpWebResponse)webRequest.GetResponse();
        }

        public static HttpWebResponse MakeNormalPOSTRequest(string url, string requestContent)
        {
            return MakePOSTRequest(CreateNormalRequest(url), requestContent);
        }

        public static HttpWebResponse MakeAuthPOSTRequest(string url, string username, string password, string requestContent)
        {
            return MakePOSTRequest(CreateRequestWithAuth(url, username, password), requestContent);
        }

        public static HttpWebResponse MakePOSTRequest(HttpWebRequest webRequest, string requestContent)
        {
            return MakePOSTRequest(webRequest, requestContent, "application/x-www-form-urlencoded");
        }

        public static HttpWebResponse MakeJsonPOSTRequest(HttpWebRequest webRequest, string requestContent)
        {
            return MakePOSTRequest(webRequest, requestContent, "application/json");
        }

        public static HttpWebResponse MakePOSTRequest(HttpWebRequest webRequest, string requestContent, string contentType)
        {
            webRequest.Method = "POST";
            webRequest.ContentType = contentType;
            if (!string.IsNullOrEmpty(requestContent))
            {
                var contentBytes = Encoding.ASCII.GetBytes(requestContent);
                webRequest.ContentLength = contentBytes.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(contentBytes, 0, contentBytes.Length);
                }
            }
            else
            {
                webRequest.ContentLength = 0;
            }
            return (HttpWebResponse)webRequest.GetResponse();
        }

        /// <summary>
        /// Uploads files via HTTP and adds text parameters too.
        /// </summary>
        /// <param name="webRequest">The HttpWebRequest object to be used.</param>
        /// <param name="textParameters">ParamName -> text value.</param>
        /// <param name="fileParameters">ParamName -> local filepath.</param>
        /// <returns>Returns the HttpWebResponse object representing the server's response to the HTTP request.</returns>
        public static HttpWebResponse HttpUploadFiles(HttpWebRequest webRequest, SSKeyValuePair[] textParameters, SSKeyValuePair[] fileParameters)
        {
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("X");
            var formDataHeader = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"";
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.KeepAlive = true;
            webRequest.Method = "POST";
            using (var requestStream = webRequest.GetRequestStream())
            {
                foreach (var textParameter in textParameters)
                {
                    var textParamHeader = formDataHeader + textParameter.Key + "\"\r\n\r\n" + textParameter.Value;
                    requestStream.WriteString(textParamHeader);
                }
                foreach (var fileParameter in fileParameters)
                {
                    AppendHttpFile(requestStream, formDataHeader, fileParameter);
                }
                requestStream.WriteString("\r\n--" + boundary + "--\r\n");
            }
            return (HttpWebResponse)webRequest.GetResponse();
        }

        private static void AppendHttpFile(Stream requestStream, string formDataHeader, SSKeyValuePair fileParameter)
        {
            var filePath = fileParameter.Value;
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var fileParamHeader = formDataHeader + fileParameter.Key + "\"; filename=\"" + Path.GetFileName(filePath) + "\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                requestStream.WriteString(fileParamHeader);
                fileStream.CopyTo(requestStream);
            }
        }

        public static string ReadResponse(HttpWebResponse webResponse)
        {
            using (var responseReader = new StreamReader(webResponse.GetResponseStream()))
            {
                return responseReader.ReadToEnd();
            }
        }

        public static bool WebExceptionHasStatusCode(WebException ex, HttpStatusCode statusCode)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var responseStatusCode = ((HttpWebResponse)ex.Response).StatusCode;
                return responseStatusCode == statusCode;
            }
            return false;
        }

        public static WebError GetWebErrorFromWebException(WebException ex)
        {
            switch (ex.Status)
            {
                case WebExceptionStatus.ConnectFailure:
                case WebExceptionStatus.ConnectionClosed:
                case WebExceptionStatus.NameResolutionFailure:
                case WebExceptionStatus.ProxyNameResolutionFailure: return WebError.ConnectionFailed;

                case WebExceptionStatus.ProtocolError:
                    HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    switch (statusCode)
                    {
                        case HttpStatusCode.Unauthorized: return WebError.Unauthorized;
                        default:
                            System.Diagnostics.Debug.Print("ex.StatusCode => " + statusCode.ToString());
                            break;
                    }
                    break;
                default:
                    System.Diagnostics.Debug.Print("ex.Status => " + ex.Status.ToString());
                    break;
            }
            return WebError.Unknown;
        }
    }

    public enum WebError
    {
        OK = 0,
        ConnectionFailed = 1,
        Unauthorized = 2,
        Unknown = -1
    }
}
