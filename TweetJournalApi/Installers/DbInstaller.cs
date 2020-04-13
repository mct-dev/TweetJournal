using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TweetJournalApi.Data;
using TweetJournalApi.Services;

namespace TweetJournalApi.Installers
{
    public class DbInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EntriesDbContext");
            services.AddDbContext<EntryContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<EntryService, EntryServiceImp>();
        }
    }
}
