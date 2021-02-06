using AutoMapper;

namespace AniraSP.DAL.Adapter.Automapper {
    public class AutoMapperConfiguration {
        public static IMapper CreateMapper() {
            return Create().CreateMapper();
        }

        public static MapperConfiguration Create() {
            var mapperConfiguration = new MapperConfiguration(cfg => {

                cfg.AddProfile<AutoMapperProfile>();
            });

            mapperConfiguration.AssertConfigurationIsValid();
            return mapperConfiguration;
        }
    }
}