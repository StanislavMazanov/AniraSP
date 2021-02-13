using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniraPS.Clients;
using AniraPS.Clients.Tinkoru;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;
using Microsoft.Extensions.Hosting;
using NLog;

namespace AniraSP.Console {
    public class ScrappyService : BackgroundService {
        private readonly IOfferStorageWorker _storage;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ScrappyService(IOfferStorageWorker storage) {
            _storage = storage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            _logger.Debug("Start");

            var bot = new TinkoruBot(_storage, new DefaultLogger(), new BotOptions());
            await bot.RunScrappy();
        }
    }
}