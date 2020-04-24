using System;
using AutoMapper;
using AutoMapper.Configuration;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;
using TweetJournal.Api.Mapping;

namespace TweetJournal.Api.Tests
{
    public static class Mother
    {
        public static CreateEntryRequest CreateEntryRequest => new CreateEntryRequest
        {
            Content = TestContent
        };
        public static Entry ContractEntry => new Entry
        {
            Content = TestContent,
            Id = TestEntryId,
            CreatedDate = TestDateTimeNow,
            ModifiedDate = TestDateTimeNow
        };
        public static EntryResponse EntryResponse => new EntryResponse
        {
            Content = TestContent,
            Id = TestEntryId,
            CreatedDate = TestDateTimeNow,
            ModifiedDate = TestDateTimeNow
        };
        
        private static string TestContent = "Testing entry content.";
        private static DateTime TestDateTimeNow = DateTime.Now;
        private static Guid TestEntryId = Guid.NewGuid();
        
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