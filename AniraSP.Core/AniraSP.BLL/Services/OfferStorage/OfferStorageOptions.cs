namespace XmlDataFeed.BLL.Services.OffersUploader {
    public class OfferStorageOptions {
        private int _offerPortions;
        private int _timeOut;

        public OfferStorageOptions() {
            TimeOut = 1000;
            OfferPortions = 3000;
        }


        /// <summary>
        /// Количество порций для записи в базу данных
        /// </summary>
        public int OfferPortions {
            get => _offerPortions;
            set => _offerPortions = value <= 0 ? 1 : value;
        }

        /// <summary>
        /// ID компании
        /// </summary>
        public int CompanyId { get; internal set; }


        /// <summary>
        /// время между проверками, по умолчанию 100мс
        /// </summary>
        public int TimeOut {
            get => _timeOut;
            set => _timeOut = value <= 0 ? 100 : value;
        }
    }
}