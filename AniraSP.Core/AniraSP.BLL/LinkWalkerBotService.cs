using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;
using AniraSP.BLL.Utils;

namespace AniraSP.BLL {
    public abstract class LinkWalkerBotService<T> : IBot
        where T : WebWorker, new() {
        private readonly IOfferStorageWorker _storage;
        private readonly ILog _eventLogger;
        private int _processedAllCount;
        private readonly ConcurrentQueue<string> _queueLinks;
        private readonly List<Task> _processors;
        private readonly CancellationTokenSource _mainCancelTokenSource = new CancellationTokenSource();
        private readonly WebWorker _webWorker;
        public Func<string, string> ModifyStringUrl { get; set; }
        protected BotOptions BotOptions;
        private HtmlParser _htmlParser;

        protected LinkWalkerBotService(IOfferStorageWorker storage, ILog eventLogger, BotOptions botOptions) {
            _storage = storage;
            _eventLogger = eventLogger;
            _processors = new List<Task>();
            BotOptions = botOptions;
            _queueLinks = new ConcurrentQueue<string>();
            _webWorker = new T();
            _htmlParser = new HtmlParser();
        }

        public abstract Task RunScrappy();

        public void GenerateYaMarketXml(string[] links) {
            if (links == null)
                return;

            Scan(links);
            _storage.LastCommit();
        }

        protected void Scan(string[] links) {
            if (links == null) return;

            foreach (string link in links) {
                _queueLinks.Enqueue(link);
            }

            _processedAllCount = links.Length;
            CancellationToken token = _mainCancelTokenSource.Token;
            for (var i = 0; i < BotOptions.NumberOfThreads; i++) {
                Task task = Task.Factory.StartNew(() => LoadPageWorker(token), TaskCreationOptions.LongRunning);
                _processors.Add(task);
            }

            Task.WaitAll(_processors.ToArray());
            _processors.Clear();
        }

        private void LoadPageWorker(CancellationToken token) {
            while (true) {
                if (_queueLinks.Count > 0) {
                    if (_queueLinks.TryDequeue(out string url))
                        WebPageLocal(url);
                }

                if (token.IsCancellationRequested || _queueLinks.Count == 0) {
                    break;
                }


                if (BotOptions.TimeOutPage > 1) {
                    Thread.Sleep(BotOptions.TimeOutPage);
                }
            }
        }

        private void WebPageLocal(string pageUrl) {
            if (string.IsNullOrEmpty(pageUrl)) return;

            try {
                string modifiedPageUrl = ModifyStringUrl?.Invoke(pageUrl) ?? pageUrl;
                GetOfferPage(modifiedPageUrl);
            }
            catch (Exception e) {
                _eventLogger?.Error(
                    $"Method:WebPageLocal, Url: {pageUrl}, Message: {e.Message} {e.StackTrace}");
            }
        }

        private void GetOfferPage(string pageUrl) {
            try {
                WebPageResponse responseData = WebUtils.GetData(pageUrl, BotOptions);
                responseData.SetHtmlParser(_htmlParser);
                if (responseData.IsSuccess) {
                    AddOfferStorage(responseData);
                }
            }
            catch (Exception e) {
                _eventLogger?.Error($"Url: {pageUrl}, Message: {e.Message} {e.StackTrace}");
            }
        }

        private void AddOfferStorage(WebPageResponse responseData) {
            if (!_webWorker.IsOfferPage(responseData)) return;

            try {
                AniraSpOffer offerPage = _webWorker.GetOfferData(responseData);
                AddOffer(offerPage);
            }
            catch (Exception e) {
                _eventLogger?.Error($"Url: {responseData.Url}, Message: {e.Message} {e.StackTrace}");
            }
        }

        private void AddOffer(AniraSpOffer offer) {
            _storage.Add(offer);
        }
    }
}