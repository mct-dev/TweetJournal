using System;

namespace TweetJournalApi.Contracts.V1.Requests
{
    public class UpdateEntryRequest
    {
        public Guid Id;
        public string Content;
    }
}
