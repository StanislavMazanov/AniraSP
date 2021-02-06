using Autofac;
using Microsoft.EntityFrameworkCore;

namespace AniraSP.DAL {
    public class DataAccessModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.Register(c => {
                    var databaseSettings = c.Resolve<IDatabaseSettings>();

                    return new DbContextOptionsBuilder<AniraSpDbContext>()
                        .UseSqlServer(databaseSettings.ConnectionString )
                        .Options;
                })
                .As<DbContextOptions<AniraSpDbContext>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AniraSpDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}