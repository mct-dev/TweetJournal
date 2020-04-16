using System.Collections.Generic;

namespace TweetJournal.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors;
    }
}