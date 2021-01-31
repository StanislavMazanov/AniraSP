using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AniraSP.Utilities.Storage {
    public abstract class StorageUploader<T> where T : class {
        private readonly ConcurrentQueue<T> _queueOffers;
        private bool _lastCommit;

        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private readonly Task _saveOffersWorker;

        public int OfferPortions { get; set; }

        public StorageUploader() {
            _queueOffers = new ConcurrentQueue<T>();
            _lastCommit = false;
            OfferPortions = 500;
            CancellationToken token = _cancelTokenSource.Token;
            _saveOffersWorker = Task.Factory.StartNew(() => WorkerLoop(token), token);
        }


        private void WorkerLoop(CancellationToken token) {
            while (true) {
                if (_queueOffers.Count >= OfferPortions) {
                    var offersToCommit = new List<T>();

                    for (var i = 0; i < OfferPortions; i++) {
                        _queueOffers.TryDequeue(out T offer);

                        if (offer != null) {
                            offersToCommit.Add(offer);
                        }
                    }

                    try {
                        UpdateToStorage(offersToCommit);
                    }
                    catch (Exception ex) {
                        //ignore
                    }

                    offersToCommit.Clear();
                }

                if ((token.IsCancellationRequested || _lastCommit) && _queueOffers.Count < OfferPortions) {
                    var offersToCommit = new List<T>();
                    while (true) {
                        if (_queueOffers.TryDequeue(out T offer)) {
                            offersToCommit.Add(offer);
                        }
                        else {
                            UpdateToStorage(offersToCommit);
                            return;
                        }

                        Thread.Sleep(1);
                    }
                }

                Thread.Sleep(1);
            }
        }

        public abstract void UpdateToStorage(List<T> offers);

        public void Add(T offer) {
            _queueOffers.Enqueue(offer);
        }

        public virtual void LastCommit() {
            _lastCommit = true;
            _saveOffersWorker.Wait();
        }

        public void AddRange(IEnumerable<T> siteCash) {
            foreach (T item in siteCash) {
                Add(item);
            }
        }

        public int Count() {
            return _queueOffers?.Count ?? 0;
        }
    }
}