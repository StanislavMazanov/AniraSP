using System;
using System.Net;


namespace HttpWebClient {
    public class HttpResponseInfo {
        public HttpResponseInfo(HttpWebRequest request) {
            try {
                var response = (HttpWebResponse) request.GetResponse();
                HttpWebResponse = response;
                Content = new HttpClientContent(response);
            }
            catch (WebException ex) {
                if (ex.Status == WebExceptionStatus.ProtocolError &&
                    ex.Response != null) {
                    HttpWebResponse = (HttpWebResponse) ex.Response;
                    Content = new HttpClientContent(HttpWebResponse);
                }
            }
        }

        public HttpWebResponse HttpWebResponse { get; set; }


        public Uri RequestUri => HttpWebResponse.ResponseUri;

        public bool IsSuccessStatusCode => HttpWebResponse?.StatusCode == HttpStatusCode.OK;


        public HttpClientContent Content { get; }

        public HttpStatusCode StatusCode => HttpWebResponse?.StatusCode ?? HttpStatusCode.BadRequest;
    }
}