using System.Collections.Generic;
using AniraSP.BLL.Models;
using AniraSP.BLL.Services.OfferStorage;
using AniraSP.DAL.Adapter;

namespace AniraSP.Console {
    public class MainStorage : IOfferStorageWorker {
        private readonly PortionUploader _portionUploader;

        public MainStorage(PortionUploader portionUploader) {
            _portionUploader = portionUploader;
        }

        public void Add(AniraSpOffer offer) {
            _portionUploader.Add(offer);
        }

        public void AddRange(IEnumerable<AniraSpOffer> offers) {
            foreach (AniraSpOffer offer in offers) Add(offer);
        }

        public void LastCommit() {
            _portionUploader.LastCommit();
        }
    }
}