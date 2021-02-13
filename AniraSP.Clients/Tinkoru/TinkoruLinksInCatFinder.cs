using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.LinksFinderInCategory;
using AniraSP.Utilities;

namespace AniraPS.Clients.Tinkoru {
    public class TinkoruLinksInCatFinder : LinksInCategoryFinderService {
        public TinkoruLinksInCatFinder(BotOptions botOptions) : base(botOptions) { }

        protected override string[] FindOffersUrlsInPage(IDocument htmlCode, Uri hostUrl, string pageUrl) {
            string[] products = htmlCode.QuerySelectorAll(".catalog-product__title a")?
                .Select(x => x.GetAttribute("href").UpToAbsoluteUrl(hostUrl))
                .Distinct().ToArray() ?? Array.Empty<string>();
            return products;
        }

        protected override string CreatePageUrl(int pageNumber, string categoryUrl) {
            return $"{categoryUrl}?PAGEN_1={pageNumber}";
        }

        public override List<string> GetCategories() {
            return new() {
                "https://www.tinko.ru/catalog/category/1/"
            };
        }
    }
}