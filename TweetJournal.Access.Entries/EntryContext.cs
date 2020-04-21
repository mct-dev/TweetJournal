using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweetJournal.Access.Entries.Domain;

namespace TweetJournal.Access.Entries
{
    public class EntryContext : IdentityDbContext
    {
        public EntryContext(DbContextOptions<EntryContext> options) : base(options)
        { }

        public DbSet<Entry> Entries { get; set; }
    }
}
