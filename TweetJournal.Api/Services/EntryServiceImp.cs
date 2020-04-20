using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetJournal.Api.Data;
using TweetJournal.Api.Domain;

namespace TweetJournal.Api.Services
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
            return await _entryContext.Entries.SingleOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<List<Entry>> GetAsync()
        {
            return await _entryContext.Entries.ToListAsync();
        }
        
        public async Task<bool> CreateAsync(Entry newEntry)
        {
            await _entryContext.Entries.AddAsync(newEntry);
            var numberOfUpdates = await _entryContext.SaveChangesAsync();
            return numberOfUpdates > 0;
        }

        public async Task<bool> UpdateAsync(Entry updatedEntry)
        {
            _entryContext.Entries.Update(updatedEntry);
            var numberOfUpdates = await _entryContext.SaveChangesAsync();
            return numberOfUpdates > 0;
        }

        public async Task<bool> DeleteAsync(Guid entryId)
        {
            var existingEntry = await GetByIdAsync(entryId);
            if (existingEntry == null)
            {
                return false;
            }

            _entryContext.Entries.Remove(existingEntry);
            var numberOfUpdates = await _entryContext.SaveChangesAsync();
            return numberOfUpdates > 0;
        }
    }
}
