using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Requests
{
    [ExcludeFromCodeCoverage]
    public class UserRegistrationRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
    }
}