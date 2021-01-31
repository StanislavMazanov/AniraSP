using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace AniraSP.BLL.Models {
    [Serializable, XmlRoot("Offer")]
    public class AniraSpOffer {
        [XmlAttribute("Id")]
        [JsonProperty("Id")]
        public string OfferId { get; set; }

        [XmlElement("OfferParams")]
        [JsonProperty("Params")]

        public List<OfferParam> OfferParams { get; set; }
    }

    public sealed class OfferParam {
        [XmlAttribute("N")]
        [JsonProperty("N")]
        public string Name { get; set; }

        [JsonProperty("V")] [XmlText] public string Value { get; set; }
    }
}