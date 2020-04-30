using System;
using System.Collections.Generic;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;
using TweetJournal.Api.Domain;

namespace TweetJournal.Api.Tests.Entries
{
    public static class Mother
    {
        public static CreateEntryRequest GenericCreateEntryRequest => new CreateEntryRequest
        {
            Content = TestContent
        };
        public static Entry GenericEntry => new Entry
        {
            Content = TestContent,
            Id = TestEntryId,
            CreatedDate = TestDateTimeNow,
            ModifiedDate = TestDateTimeNow
        };
        public static EntryResponse GenericEntryResponse => new EntryResponse
        {
            Content = TestContent,
            Id = TestEntryId,
            CreatedDate = TestDateTimeNow,
            ModifiedDate = TestDateTimeNow
        };

        public static UpdateEntryRequest UpdateEntryRequest => new UpdateEntryRequest
        {
            Content = "Updated entry!"
        };
        public static HydratedUpdateEntryRequest HydratedUpdateEntryRequest => new HydratedUpdateEntryRequest
        {
            Id = GenericEntry.Id,
            Content = GenericEntry.Content,
            ModifiedDate = DateTime.Now
        };
        public static Entry UpdatedEntry => new Entry
        {
            Id = GenericEntry.Id,
            Content = UpdateEntryRequest.Content,
            ModifiedDate = HydratedUpdateEntryRequest.ModifiedDate,
            CreatedDate = GenericEntry.CreatedDate
        };
        public static EntryResponse UpdatedEntryResponse => new EntryResponse
        {
            Id = GenericEntry.Id,
            Content = UpdatedEntry.Content,
            ModifiedDate = UpdatedEntry.ModifiedDate,
            CreatedDate = UpdatedEntry.CreatedDate
        };

        private const string TestContent = "Testing entry content.";
        private static readonly DateTime TestDateTimeNow = DateTime.Now;
        public static readonly Guid TestEntryId = Guid.NewGuid();

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
    }
}