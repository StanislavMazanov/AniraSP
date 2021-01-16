namespace AniraSP.BLL.Models {
    public abstract class WebWorker {
        public abstract AniraSpOffer GetOfferData(WebPageResponse webPageResponse);

        public abstract bool IsOfferPage(WebPageResponse webPageResponse);
    }
}