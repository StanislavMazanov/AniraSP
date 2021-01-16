using System;
using System.Collections.Generic;
using AngleSharp;
using AniraSP.BLL.Models;

namespace AniraPS.Clients {
    public class TinkoruWebWorker : WebWorker {
        private string _productId;

        public override bool IsOfferPage(WebPageResponse webPageResponse) {
            try {
                string productId = webPageResponse.HtmlDocument.QuerySelector("span.product-detail__price-value")
                    ?.TextContent;
                if (!string.IsNullOrEmpty(productId)) {
                    _productId = productId;
                    return true;
                }
            }
            catch (Exception exception) {
                // EventLogger.Error($"Error! Url:{Url.OriginalString}, Method:ProductId, Message:{exception.Message}");
            }

            return false;
        }

        public override AniraSpOffer GetOfferData(WebPageResponse webPageResponse) {
            var param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("Price", "25.00"));

            var offer = new AniraSpOffer {
                Id = _productId,
                Params = param
            };
            return offer;
        }
    }
}