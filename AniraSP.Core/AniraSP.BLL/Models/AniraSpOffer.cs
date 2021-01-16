using System.Collections.Generic;

namespace AniraSP.BLL.Models {
    public class AniraSpOffer {
        public string Id { get; set; }
        public List<KeyValuePair<string, string>> Params { get; set; }
    }
}