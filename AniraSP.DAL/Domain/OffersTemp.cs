using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AniraSP.DAL.Domain {
    [Table("OffersTemp")]
    public class OffersTemp {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int SiteId { get; set; }

        public string OfferId { get; set; }

        public string OfferInfo { get; set; }

        public DateTime CreationDate { get; set; }
    }
}