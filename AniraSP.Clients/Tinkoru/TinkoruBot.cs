using System.Collections.Generic;
using System.Threading.Tasks;
using AniraSP.BLL;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;

namespace AniraPS.Clients.Tinkoru {
    public class TinkoruBot : LinkWalkerBotService<TinkoruWebWorker> {
        public TinkoruBot(IOfferStorageWorker storage, ILog eventLogger, BotOptions botOptions) : base(storage,
            eventLogger, botOptions) {
            BotOptions.TimeOutPage = 10000;


        }

        public override Task RunScrappy() {
            string[] links = new TinkoruLinksInCatFinder(BotOptions).GetProductsLinks();
            GenerateYaMarketXml(links);
            return Task.CompletedTask;
        }
    }
}