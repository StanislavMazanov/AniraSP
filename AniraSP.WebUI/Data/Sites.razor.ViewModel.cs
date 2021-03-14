using System.Threading.Tasks;

namespace AniraSP.WebUI.Data {
    public class SiteViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Shard { get; set; }
        public string StartTime { get; set; }


    }
}