using System;
using TweetJournal.Api.Contracts.V1.Requests;

namespace TweetJournal.Api.Domain
{
    public class HydratedUpdateEntryRequest : UpdateEntryRequest
    {
        public Guid Id { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}