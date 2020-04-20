using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweetJournal.Api.Domain;

namespace TweetJournal.Api.Data
{
    public class EntryContext : IdentityDbContext
    {
        public EntryContext(DbContextOptions<EntryContext> options) : base(options)
        { }

        public DbSet<Entry> Entries { get; set; }
    }
}
