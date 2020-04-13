using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetJournalApi.Data;
using TweetJournalApi.Domain;

namespace TweetJournalApi.Services
{
    public class EntryServiceImp : EntryService
    {
        private readonly EntryContext _entryContext;

        public EntryServiceImp(EntryContext entryContext)
        {
            _entryContext = entryContext;
        }

        public async Task<Entry> GetByIdAsync(Guid id)
        {
            return await _entryContext.Entries.FindAsync(id);
        }
        
        public async Task<List<Entry>> GetAsync()
        {
            return await _entryContext.Entries.ToListAsync();
        }
        
        public async Task<bool> CreateAsync(Entry newEntry)
        {
            await _entryContext.Entries.AddAsync(newEntry);
            await _entryContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Entry updatedEntry)
        {
            var exists = await _entryContext.Entries.FindAsync(updatedEntry.Id);
            if (exists == null)
            {
                return false;
            }

            _entryContext.Entries.Update(updatedEntry);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid entryId)
        {
            var existingEntry = await _entryContext.Entries.FindAsync(entryId);
            if (existingEntry == null)
            {
                return false;
            }

            _entryContext.Entries.Remove(existingEntry);
            return true;
        }
    }
}
