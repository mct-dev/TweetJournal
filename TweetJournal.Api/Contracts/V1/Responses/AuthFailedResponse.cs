using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Contracts.V1.Responses
{
    [ExcludeFromCodeCoverage]
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors;
    }
}