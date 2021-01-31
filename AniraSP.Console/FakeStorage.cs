using System.Collections.Generic;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;

namespace AniraSP.Console {
    public class FakeStorage : IOfferStorageWorker {
        public void Add(AniraSpOffer offer) {
            System.Console.WriteLine($"{offer.OfferId}");
        }

        public void AddRange(IEnumerable<AniraSpOffer> offers) {
            foreach (AniraSpOffer offer in offers) {
                Add(offer);
            }
        }

        public void LastCommit() {
            System.Console.WriteLine($"It's last commit");
        }
    }
}