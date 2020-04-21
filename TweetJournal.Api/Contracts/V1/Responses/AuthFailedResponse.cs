using System.Collections.Generic;

namespace TweetJournal.Api.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors;
    }
}