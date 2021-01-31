using System.Collections.Generic;
using AniraSP.BLL;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;

namespace AniraSP.PerformanceTest {
    public class OfferStorageWorker:IOfferStorageWorker {
        private readonly PortionUploader _portionUploader;

        public OfferStorageWorker(PortionUploader portionUploader) {
            _portionUploader = portionUploader;
        }

        public void Add(AniraSpOffer offer) {
            _portionUploader.Add(offer);
        }

        public void AddRange(IEnumerable<AniraSpOffer> offers) {
            _portionUploader.AddRange(offers);

        }

        public void LastCommit() {
            _portionUploader.LastCommit();
        }
    }
}