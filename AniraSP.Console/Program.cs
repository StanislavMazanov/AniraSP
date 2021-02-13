using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AniraSP.Console {
    class Program {
        static async Task Main(string[] args) {
            // IConfigurationRoot config = new ConfigurationBuilder()
            //     .SetBasePath(System.IO.Directory
            //         .GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //     .Build();
            await HostStartUp.CreateHostBuilder().RunConsoleAsync();
        }
    }
}