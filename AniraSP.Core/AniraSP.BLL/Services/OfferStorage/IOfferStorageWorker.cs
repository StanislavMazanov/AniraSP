using System.Collections.Generic;
using AniraSP.BLL.Models;

namespace AniraSP.BLL.Services.OfferStorage {
    public interface IOfferStorageWorker {
        void Add(AniraSpOffer offer);
        void AddRange(IEnumerable<AniraSpOffer> offers);
        void LastCommit();
    }
}