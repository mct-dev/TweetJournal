using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Requests
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}