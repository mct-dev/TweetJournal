using System;
using System.Collections.Generic;
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

        private const string TestContent = "Testing entry content.";
        private static readonly DateTime TestDateTimeNow = DateTime.Now;
        private static readonly Guid TestEntryId = Guid.NewGuid();

        public static IEnumerable<Entry> GetTestEntriesList()
        {
            return new List<Entry>
            {
                new Entry
                {
                    Id = Guid.NewGuid(),
                    Content = "Test content 1",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now.AddDays(2)
                },
                new Entry
                {
                    Id = Guid.NewGuid(),
                    Content = "Test content 2",
                    CreatedDate = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                    ModifiedDate = DateTime.Now.Subtract(TimeSpan.FromDays(1))
                }
            };
        }
        
        public static IEnumerable<EntryResponse> GetTestEntryResponseList()
        {
            return new List<EntryResponse>
            {
                new EntryResponse
                {
                    Id = Guid.NewGuid(),
                    Content = "Test content 1",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now.AddDays(2)
                },
                new EntryResponse
                {
                    Id = Guid.NewGuid(),
                    Content = "Test content 2",
                    CreatedDate = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                    ModifiedDate = DateTime.Now.Subtract(TimeSpan.FromDays(1))
                }
            };
        }
        
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