using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateEntryRequest
    {
        public string Content { get; set; }
    }
}
