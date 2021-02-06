namespace AniraSP.DAL.Adapter {
    public class DatabaseSettings : IDatabaseSettings {
        public DatabaseSettings() {
            ConnectionString = "Server=localhost;initial catalog=AniraSP;Integrated Security=true";
        }

        public string ConnectionString { get; set; }
    }
}