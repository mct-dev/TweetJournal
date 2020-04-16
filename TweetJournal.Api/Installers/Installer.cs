using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TweetJournalApi.Installers
{
    public interface Installer
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
