using System.Threading;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;
using AniraSP.DAL.Repository;

namespace AniraSP.PerformanceTest {
    public class Service {
        private readonly IOfferStorageWorker _offerStorageWorker;
        private readonly OfferGenerator _offerGenerator;

        public Service(IOfferStorageWorker offerStorageWorker, OfferGenerator offerGenerator) {
            _offerStorageWorker = offerStorageWorker;
            _offerGenerator = offerGenerator;
        }

        public void Run() {
            for (var i = 0; i < 1500000; i++) {
                AniraSpOffer[] offers = _offerGenerator.Generate(1, i);
                _offerStorageWorker.AddRange(offers);
                //Thread.Sleep(1);
            }

            _offerStorageWorker.LastCommit();
        }
    }
}