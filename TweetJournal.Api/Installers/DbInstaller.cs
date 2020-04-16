using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EntryContext>();
            services.AddScoped<EntryService, EntryServiceImp>();
            services.AddScoped<IdentityService, IdentityServiceImp>();
        }
    }
}
