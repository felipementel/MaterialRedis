using AutoMapper;

namespace Aplicacao.Application.AutoMapperConfigs
{
    public class AutoMapperConfig
    {
        protected AutoMapperConfig() { }

        public static MapperConfiguration RegisterMappings() =>
            new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new AplicacaoProfile());
            });
    }
}