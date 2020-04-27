using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Access.Authentication.Contract
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}