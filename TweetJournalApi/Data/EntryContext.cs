using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweetJournalApi.Domain;

namespace TweetJournalApi.Data
{
    public class EntryContext : IdentityDbContext
    {
        public EntryContext(DbContextOptions<EntryContext> options) : base(options)
        { }

        public DbSet<Entry> Entries { get; set; }
    }
}
