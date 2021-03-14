using AniraSP.DAL;
using Microsoft.Extensions.Configuration;

namespace AniraSP.WebUI.AppStart {
    public class DatabaseSettings : IDatabaseSettings {
        public DatabaseSettings(IConfiguration configuration) {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string ConnectionString { get; set; }
    }
}