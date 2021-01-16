using System.Collections.Generic;
using AniraPS.Clients;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;

namespace AniraSP.Console {
    public class ScrappyService {
        public void Run() {
            var storage = new FakeStorage();
            var bot = new TinkoruBot(storage, new DefaultLogger(), new BotOptions());
            bot.RunScrappy();
        }
    }

    public class FakeStorage : IOfferStorageWorker {
        public void Add(AniraSpOffer offer) {
            System.Console.WriteLine($"{offer.Id}");
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