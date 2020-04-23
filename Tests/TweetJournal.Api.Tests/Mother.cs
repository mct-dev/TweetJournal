using System;
using AutoMapper;
using AutoMapper.Configuration;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Mapping;

namespace TweetJournal.Api.Tests
{
    public static class Mother
    {
        public static CreateEntryRequest TestCreateEntryRequest => new CreateEntryRequest
        {
            Content = "Testing entry content."
        };
        public static Entry ContractEntry => new Entry
        {
            Content = "Testing entry content.",
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
        
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