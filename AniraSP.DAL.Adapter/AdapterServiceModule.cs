using AniraSP.DAL.Adapter.Automapper;
using AniraSP.DAL.Handles;
using Autofac;
using AutoMapper;

namespace AniraSP.DAL.Adapter {
    public class AdapterServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<AniraSpDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PortionUploader>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BulkCopyUploader>().As<IBulkCopyUploader>().InstancePerLifetimeScope();

            builder.RegisterType<DatabaseSettings>().As<IDatabaseSettings>().InstancePerLifetimeScope();

            builder.Register(_ => AutoMapperConfiguration.Create());
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>()
                .InstancePerLifetimeScope();
            builder.RegisterModule<DataAccessModule>();
        }
    }
}