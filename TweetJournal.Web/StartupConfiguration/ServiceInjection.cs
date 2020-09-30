using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TweetJournal.Web.StartupConfiguration
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceInjection
    {
        public static void InjectServices(IServiceCollection services, IConfiguration configuration)
        {
            Access.Entries.ServiceInjection.ConfigureServices(services, configuration);
        }
    }
}