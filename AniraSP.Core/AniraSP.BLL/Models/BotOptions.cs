using System;
using AniraSP.BLL.Services.ProxyServices;

namespace AniraSP.BLL.Models {
    public class BotOptions {
        public BotOptions() {
            NumberOfThreads = 1;
            TimeOutPage = 1;
            OffersPortion = 3000;
            UseProxy = false;
            ShouldUpdateCookies = true;
            HttpClientContext = new HttpClientOptions();
        }

        public bool ShouldUpdateCookies { get; set; }
        public int OffersPortion { get; set; }
        public HttpClientOptions HttpClientContext { get; set; }
        public IProxyContainer ProxyContainer { get; set; }
        public int NumberOfThreads { get; set; }
        public int TimeOutPage { get; set; }

        public bool UseProxy { get; set; }

        public bool IsRemoveUrlQuery = false;
    }
}