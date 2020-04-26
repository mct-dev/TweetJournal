using AutoMapper;
using AutoMapper.Configuration;
using TweetJournal.Api.Mapping;

namespace TweetJournal.Api.Tests.Mapping
{
    public static class Mother
    {
        public static IMapper ConfigureAutoMapper()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile<MappingProfile>();
            
            var mapperConfig = new MapperConfiguration(cfg);
            IMapper mapper = new Mapper(mapperConfig);
            
            return mapper;
        }

        public static MapperConfiguration GetAutoMapperConfiguration()
        {
            return AutoMapperConfiguration.ConfigureMapper();
        }
    }
}