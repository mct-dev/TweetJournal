using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Options
{
    [ExcludeFromCodeCoverage]
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UIEndpoint { get; set; }
    }
}
