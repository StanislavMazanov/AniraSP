using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AniraSP.DAL.Domain.Sites {
    [Table("Sites")]
    public class Site {
        public Site() {
            Settings = "{ }";
        }
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Shard { get; set; }
        public string SiteUrl { get; set; }
        public string Settings { get; set; }

        [NotMapped]
        public SiteSettings SettingsData {
            get => JsonConvert.DeserializeObject<SiteSettings>(Settings);
            set => Settings = JsonConvert.SerializeObject(value);
        }
    }
}