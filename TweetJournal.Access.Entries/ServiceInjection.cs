using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TweetJournal.Access.Entries
{
    public static class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EntriesDbContext");

            services.AddDbContext<EntryContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IEntryAccess, EntryAccess>();
        }
    }
}