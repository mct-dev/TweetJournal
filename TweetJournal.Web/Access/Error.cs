using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TweetJournal.Web.Access
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        [JsonProperty("error")]
        public string Message { get; set; }
    }
}
