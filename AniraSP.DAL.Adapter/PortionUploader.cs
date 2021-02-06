using System.Collections.Generic;
using AniraSP.BLL.Models;
using AniraSP.DAL.Domain;
using AniraSP.Utilities.Storage;
using AutoMapper;

namespace AniraSP.DAL.Adapter {
    public class PortionUploader : StorageUploader<AniraSpOffer> {
        private readonly IBulkCopyUploader _bulkCopyUploader;
        private readonly IMapper _mapper;

        public PortionUploader(IBulkCopyUploader bulkCopyUploader, IMapper mapper) {
            _bulkCopyUploader = bulkCopyUploader;
            _mapper = mapper;
        }

        public override void UpdateToStorage(List<AniraSpOffer> offers) {
            OffersTemp[] offersTempDbModel = _mapper.Map<OffersTemp[]>(offers);
            _bulkCopyUploader.Upload(offersTempDbModel);
        }
    }
}