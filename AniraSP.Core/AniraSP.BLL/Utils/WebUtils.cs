using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AniraSP.BLL.Models;
using HttpWebClient;

namespace AniraSP.BLL.Utils {
    public static class WebUtils {
        private static HttpWebClientV2 InitHttpClient(BotOptions botOptions) {
            return new() {
                UseProxy = botOptions.UseProxy,
                DecompressionMethod = botOptions.HttpClientContext.PageDecompression,
                UserAgent = botOptions.HttpClientContext.UserAgent,
            };
        }


        public static WebPageResponse GetData(string pageUrl, BotOptions botOptions) {
            HttpWebClientV2 client = InitHttpClient(botOptions);


            Exception exception = null;
            HttpResponseInfo result = null;
            var counter = 0;
            do {
                try {
                    result = client.GetResponse(pageUrl);
                }
                catch (Exception e) {
                    exception = e;
                    Console.WriteLine(e);
                }

                counter++;
            } while (counter < 10 && result == null);


            if (result == null || result.StatusCode != HttpStatusCode.OK) return null;

            return new WebPageResponse {
                ByteContent = result.Content.ReadAsByte(),
                Content = result.Content.ReadAsString(),
                HttpStatusCode = result.StatusCode,
                Url = new Uri(pageUrl)
            };
        }
    }
}