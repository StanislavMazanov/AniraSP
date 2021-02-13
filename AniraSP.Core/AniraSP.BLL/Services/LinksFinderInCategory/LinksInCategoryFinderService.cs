using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AniraSP.BLL.Models;
using AniraSP.BLL.Utils;
using AniraSP.Utilities;
using NLog;

namespace AniraSP.BLL.Services.LinksFinderInCategory {
    public abstract class LinksInCategoryFinderService {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(LinksInCategoryFinderService));
        private readonly HtmlParser _htmlParser;
        public BotOptions BotOptions { get; }

        protected LinksInCategoryFinderService(BotOptions botOptions) {
            BotOptions = botOptions;
            _htmlParser = new HtmlParser();
        }

        public string[] GetProductsLinks() {
            List<string> categories = GetCategories();
            var linksContainer = new List<string>();
            string[] links = categories.AsParallel()
                .SelectMany(GetLinksFromCategory)
                .WithDegreeOfParallelism(BotOptions.NumberOfThreads)
                .ToArray();

            linksContainer.AddRange(links);
            return linksContainer.Distinct().ToArray();
        }

        private List<string> GetLinksFromCategory(string categoryUrl) {
            Thread.Sleep(BotOptions.TimeOutPage);

            var links = new Dictionary<string, string>();
            var pageNumber = 1;
            string pageUrl = CreatePageUrl(pageNumber, categoryUrl);
            IDocument htmlCode = AttemptWrapper(pageUrl);
            var hostUrl = new Uri(categoryUrl);
            int count;

            do {
                if (htmlCode == null) {  
                    Console.WriteLine($"htmlCode is null ");
                    break;
                }

                string[] offers = FindOffersUrlsInPage(htmlCode, hostUrl, pageUrl);

                if (offers == null) {
                    Logger.Trace($"In page number:{pageNumber} offers are null");
                    break;
                }

                if (!offers.Any()) {
                    Logger.Trace($"In page number:{pageNumber} offer collections are empty");
                    break;
                }

                count = offers.Count(l => links.AddIfNotExists(l, l));

                if (count == 0) break;

                pageNumber++;
                pageUrl = CreatePageUrl(pageNumber, categoryUrl);
                htmlCode = AttemptWrapper(pageUrl);

                Logger.Debug($"Find {count} offers in page number:{pageNumber}. Total:{links.Count}");

                Thread.Sleep(BotOptions.TimeOutPage);
            } while (count > 0);

            return links.Select(x => x.Value).ToList();
        }


        private IDocument AttemptWrapper(string pageUrl) {
            for (var i = 1; i < 3; i++) {
                try {
                    WebPageResponse htmlCode = GetDocument(pageUrl);


                    if (!htmlCode.IsSuccess) continue;

                    htmlCode.SetHtmlParser(_htmlParser);
                    return htmlCode.HtmlDocument;
                }
                catch (Exception ex) {
                    Logger.Warn($"{ex.Message}");
                }

                Logger.Warn($"Attempt №{i}");
            }
            return null;
        }

        private WebPageResponse GetDocument(string pageUrl) {
            return WebUtils.GetData(pageUrl, BotOptions);
        }


        protected abstract string[] FindOffersUrlsInPage(IDocument htmlCode, Uri hostUrl, string pageUrl);
        protected abstract string CreatePageUrl(int pageNumber, string categoryUrl);

        public abstract List<string> GetCategories();
    }
}