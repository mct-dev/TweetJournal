using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetJournal.Access.Entries.Domain;

namespace TweetJournal.Access.Entries
{
    public interface IEntryAccess
    {
        Task<IEnumerable<Entry>> GetAsync();
        Task<Entry> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Entry newEntry);
        Task<bool> UpdateAsync(Entry updatedEntry);
        Task<bool> DeleteAsync(Guid entryId);
    }
}
