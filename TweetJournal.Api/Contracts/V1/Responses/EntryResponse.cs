using System;
using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Responses
{
    [ExcludeFromCodeCoverage]
    public class EntryResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
