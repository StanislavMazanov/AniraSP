using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace HttpWebClient {
    public class HttpWebClientV2 : IDisposable {
        public bool UseProxy { get; set; }
        public string Proxy { get; set; }

        public bool AllowAutoRedirect { get; set; }

        public DecompressionMethods DecompressionMethod { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public string UserAgent { get; set; }

        /// <summary>
        /// Нужно ли использовать учетные данные для подключения
        /// </summary>
        public bool UseCredential { get; set; }

        public List<KeyValuePair<string, string>> Headers { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public HttpWebClientV2() {
            AllowAutoRedirect = true;

            SetServicePointManager();
        }

        private static void SetServicePointManager() {
            ServicePointManager.SetTcpKeepAlive(true, 1000, 10000);

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.DefaultConnectionLimit = 200;
        }

        public HttpResponseInfo GetResponse(Uri url) {
            return GetResponse(url.AbsoluteUri);
        }

        public HttpResponseInfo GetResponse(string url) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            InitRequest(request);
            request.Method = "GET";
            request.KeepAlive = true;
            return new HttpResponseInfo(request);
        }


        public HttpResponseInfo Post(string url, List<KeyValuePair<string, string>> contents,
            HttpClientContentType contentTypeEnum) {
            var uri = new Uri(url);
            return Post(uri, contents, contentTypeEnum);
        }

        public HttpResponseInfo Post(Uri url, List<KeyValuePair<string, string>> postData,
            HttpClientContentType contentTypeEnum) {
            string[] arrayPostData = postData.Select(x => $"{x.Key}={x.Value}").ToArray();
            string stringPostData = string.Join("&", arrayPostData);

            return Post(url, stringPostData, contentTypeEnum);
        }

        public HttpResponseInfo Post(string url, string postData, HttpClientContentType contentTypeEnum) {
            var uri = new Uri(url);
            return Post(uri, postData, contentTypeEnum);
        }


        public HttpResponseInfo Post(Uri url, string postData,
            HttpClientContentType contentTypeEnum) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            InitRequest(request);
            request.Method = "POST";
            request.ContentType = ContentTypeRequest(contentTypeEnum);
            //request.ContentLength = postData.Length;

            using (Stream s = request.GetRequestStream()) {
                using (var sw = new StreamWriter(s)) {
                    sw.Write(postData);
                }
            }

            return new HttpResponseInfo(request);
        }

        public HttpResponseInfo Put(string url, byte[] data) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            InitRequest(request);
            request.Timeout = 300000;
            request.Method = "PUT";
            request.ContentType = "application/binary";
            request.KeepAlive = true;

            using (Stream rs = request.GetRequestStream()) {
                rs.Write(data, 0, data.Length);
            }

            return new HttpResponseInfo(request);
        }

        public HttpResponseInfo Propfind(string url) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            InitRequest(request);
            request.Method = "PROPFIND";
            // request.ContentType = "application/binary";
            request.KeepAlive = true;
            return new HttpResponseInfo(request);
        }

        /// <summary>
        /// MKCOL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public HttpResponseInfo Mkcol(string url) {
            var request = (HttpWebRequest) WebRequest.Create(url);
            InitRequest(request);
            request.Method = "MKCOL";
            request.KeepAlive = true;
            return new HttpResponseInfo(request);
        }


        private void InitRequest(HttpWebRequest request) {
            request.UserAgent = UserAgent;
            request.CookieContainer = CookieContainer;
            request.AllowAutoRedirect = AllowAutoRedirect;
            request.KeepAlive = true;

            //request.ProtocolVersion = HttpVersion.Version10;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.UseDefaultCredentials = true;
            // request.ClientCertificates = ClientCertificateOption.Automatic;
            request.Timeout = 15000;
            request.ReadWriteTimeout = 10000;
            request.ContinueTimeout = 350;
            request.AutomaticDecompression = DecompressionMethod;

            if (UseProxy) {
                request.Proxy = new WebProxy(Proxy, false);
                request.KeepAlive = false;
            }


            request.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
            if (UseCredential) request.Credentials = new NetworkCredential(Login, Password);

            if (Headers == null) return;

            foreach ((string key, string value) in Headers)
                switch (key) {
                    case "Referer":
                        request.Referer = value;
                        break;
                    case "Accept":
                        request.Accept = value;
                        break;
                    default:
                        request.Headers.Add(key, value);
                        break;
                }
        }


        private static string ContentTypeRequest(HttpClientContentType contentType) {
            string s;
            switch (contentType) {
                case HttpClientContentType.FormData:
                    s = "application/x-www-form-urlencoded";
                    break;
                case HttpClientContentType.TextPlain:
                    s = "text/plain";
                    break;
                case HttpClientContentType.Json:
                    s = "application/json";
                    break;
                case HttpClientContentType.XmlApplication:
                    s = "application/xml";
                    break;
                case HttpClientContentType.XmlText:
                    s = "text/xml";
                    break;
                default:
                    s = "text/plain";
                    break;
            }

            return s;
        }

        public void Dispose() { }
    }
}