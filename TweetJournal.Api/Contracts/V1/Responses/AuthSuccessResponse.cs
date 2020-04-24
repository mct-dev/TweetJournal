using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Responses
{
    [ExcludeFromCodeCoverage]
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}