using System.Collections.Generic;
using AniraSP.DAL.Domain;

namespace AniraSP.DAL {
    public interface IBulkCopyUploader {
        void Upload(OffersTemp[] offers);
    }
}