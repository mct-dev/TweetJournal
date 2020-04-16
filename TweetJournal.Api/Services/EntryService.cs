
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetJournalApi.Domain;

namespace TweetJournalApi.Services
{
    public interface EntryService
    {
        Task<List<Entry>> GetAsync();
        Task<Entry> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Entry newEntry);
        Task<bool> UpdateAsync(Entry updatedEntry);
        Task<bool> DeleteAsync(Guid entryId);
    }
}
