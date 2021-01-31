using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniraPS.Clients;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;
using Microsoft.Extensions.Hosting;

namespace AniraSP.Console {
    public class ScrappyService : BackgroundService {
        private readonly IOfferStorageWorker _storage;

        public ScrappyService(IOfferStorageWorker storage) {
            _storage = storage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            // var storage = new FakeStorage();
            var bot = new TinkoruBot(_storage, new DefaultLogger(), new BotOptions());
            await bot.RunScrappy();
        }
    }
}