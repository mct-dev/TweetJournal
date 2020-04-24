using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdateEntryRequest
    {
        public string Content { get; set; }
    }
}
