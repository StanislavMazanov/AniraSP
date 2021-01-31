using System.Collections.Generic;
using AniraSP.BLL.Models;
using AniraSP.DAL.Domain;

namespace AniraSP.PerformanceTest {
    public class OfferGenerator {
        public AniraSpOffer[] Generate(int count, int index) {
            var offers = new AniraSpOffer[count];

            for (var i = 0; i < count; i++) {
                offers[i] = new AniraSpOffer {
                    OfferId = (i+index).ToString(),
                    OfferParams = new List<OfferParam> {
                        new OfferParam {Name = "OfferId", Value = (i+index).ToString()},
                        new OfferParam {Name = "Color", Value = (i+index).ToString()},
                        new OfferParam {Name = "Size", Value = (i+index).ToString()},
                        new OfferParam {Name = "Width", Value = (i+index).ToString()},
                        new OfferParam {Name = "Long", Value = (i+index).ToString()},
                    }
                };
            }

            return offers;
        }
    }
}