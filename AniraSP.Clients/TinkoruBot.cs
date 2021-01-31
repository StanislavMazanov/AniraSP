using System.Threading.Tasks;
using AniraSP.BLL;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;

namespace AniraPS.Clients {
    public class TinkoruBot : LinkWalkerBotService<TinkoruWebWorker> {
        public TinkoruBot(IOfferStorageWorker storage, ILog eventLogger, BotOptions botOptions) : base(storage,
            eventLogger, botOptions) { }

        public override Task RunScrappy() {
            var links = new[] {"https://www.tinko.ru/catalog/product/212111/"};
            GenerateYaMarketXml(links);
            return Task.CompletedTask;
        }
    }
}