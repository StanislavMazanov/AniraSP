using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AniraSP.BLL.Logging;
using AniraSP.BLL.Models;
using XmlDataFeed.BLL.Services.OffersUploader;

namespace AniraSP.BLL.Services.OfferStorage {
    public class OfferPortionStorageWrapperService : IOfferStorageWorker {
        private readonly ConcurrentDictionary<string, AniraSpOffer> _queueOffers;

        private bool _lastCommit;
        private readonly Task _saveOffersWorker;

        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private readonly IOfferStorageWorker _offerStorage;
        private readonly OfferStorageOptions _options;

        public int Count => _queueOffers.Count;
        public int OfferPortions { get; set; }

        public OfferPortionStorageWrapperService(IOfferStorageWorker offerStorage) : this(offerStorage,
            new OfferStorageOptions
                {CompanyId = 0, OfferPortions = 3000, TimeOut = 1}) { }


        public OfferPortionStorageWrapperService(IOfferStorageWorker offerStorage, OfferStorageOptions options) {
            OfferPortions = options.OfferPortions;

            _offerStorage = offerStorage;
            _options = options;
            _queueOffers = new ConcurrentDictionary<string, AniraSpOffer>();
            _lastCommit = false;

            //Start
            CancellationToken token = _cancelTokenSource.Token;
            _saveOffersWorker = Task.Factory.StartNew(() => WorkerLoop(token), TaskCreationOptions.LongRunning);
        }


        /// <summary>
        /// В случае ошибки от базы данных пробуем подключиться еще раз
        /// </summary>
        private void AttemptWrapperOffersUpdate(List<AniraSpOffer> offersToCommit) {
            Exception exception = null;

            TimeSpan timeDelay = TimeSpan.FromMinutes(1);

            //Счетчик попыток
            for (var i = 0; i < 4; i++) {
                try {
                    // XmlDataFeedOfferService.OffersUpdateV2(offersToCommit, _options.CompanyId);
                    return;
                }
                catch (Exception ex) {
                    exception = ex;
                    //Пробуем через некоторое время
                    Thread.Sleep(timeDelay);
                }
            }

            if (exception == null) return;

            ILog log = LogProvider.GetLogger();
            log?.Error(
                $"Class OfferStorageService. CompanyId = {_options.CompanyId}. Закончились попытки вставки.Error {exception.Message}.");
        }


        private void WorkerLoop(CancellationToken token) {
            while (true) {
                if (_queueOffers.Count >= OfferPortions) {
                    List<AniraSpOffer> offersToCommit = _queueOffers.Take(OfferPortions).Select(x => x.Value).ToList();
                    foreach (AniraSpOffer item in offersToCommit) {
                        _queueOffers.TryRemove(item.Id, out AniraSpOffer _);
                    }

                    AttemptWrapperOffersUpdate(offersToCommit);
                    Console.WriteLine("*");
                    offersToCommit.Clear();
                }

                //Если пришел последний коммит или отмена, перекачиваем оставшиеся офферы и заканчиваем работу
                if ((token.IsCancellationRequested || _lastCommit) && _queueOffers.Count < _options.OfferPortions) {
                    while (true) {
                        List<AniraSpOffer> offersToCommit = _queueOffers.Select(x => x.Value).ToList();
                        foreach (AniraSpOffer item in offersToCommit) {
                            _queueOffers.TryRemove(item.Id, out AniraSpOffer _);
                        }

                        AttemptWrapperOffersUpdate(offersToCommit);
                        return;
                    }
                }

                Thread.Sleep(_options.TimeOut);
            }
        }

        /// <summary>
        /// Говорим что пришел последний оффер и пора заканчивать работу
        /// </summary>
        public void LastCommit() {
            _lastCommit = true;

            _saveOffersWorker.Wait();
        }

        public void AddRange(IEnumerable<AniraSpOffer> offers) {
            foreach (AniraSpOffer offer in offers) {
                Add(offer);
            }
        }


        public void Add(AniraSpOffer offer) {
            AddOffer(offer);
        }

        private void AddOffer(AniraSpOffer offer) {
            if (offer == null) return;

            _queueOffers.AddOrUpdate(offer.Id, offer, (_, _) => offer);
        }
    }
}