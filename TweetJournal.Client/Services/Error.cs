using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TweetJournal.Client.Services
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        [JsonProperty("error")]
        public string Message { get; set; }
    }
}
