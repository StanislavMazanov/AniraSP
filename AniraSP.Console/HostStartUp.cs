using AniraSP.BLL.Services.OfferStorage;
using AniraSP.DAL.Adapter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace AniraSP.Console {
    public static class HostStartUp {
        public static IHostBuilder CreateHostBuilder() {
            return Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(AutoFacConfigure)
                // .ConfigureLogging(loggingBuilder => {
                //     loggingBuilder.ClearProviders();
                //     loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //     loggingBuilder.AddNLog(config);
                // })
                .ConfigureServices((hostContext, services) => {
                    services.AddHostedService<ScrappyService>();
                });
        }

        private static void AutoFacConfigure(ContainerBuilder builder) {
            builder.RegisterModule<AdapterServiceModule>();
            builder.RegisterModule<AdapterServiceModule>();
            builder.RegisterType<MainStorage>().As<IOfferStorageWorker>();
        }
    }
}