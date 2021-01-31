using System;
using System.IO;
using AniraSP.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AniraSP.PerformanceTest {
    class Program {
        static void Main(string[] args) {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AniraSpDbContext>();
            DbContextOptions<AniraSpDbContext> options = optionsBuilder.UseSqlServer(connectionString)
                .Options;
            var aniraSpDbContext = new AniraSpDbContext(options);
            var offerStorageWorker = new OfferStorageWorker(new PortionUploader(aniraSpDbContext));
            var offerGenerator = new OfferGenerator();
            var service = new Service(offerStorageWorker, offerGenerator);
            service.Run();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}