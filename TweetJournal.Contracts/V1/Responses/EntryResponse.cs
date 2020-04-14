using System;

namespace TweetJournal.Contracts.V1.Responses
{
    public class EntryResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
