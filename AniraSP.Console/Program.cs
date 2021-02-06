using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AniraSP.BLL.Services.OfferStorage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AniraSP.Console {
    class Program {
        static async Task Main(string[] args) {
            await HostStartUp.CreateHostBuilder().RunConsoleAsync();
        }
    }
}