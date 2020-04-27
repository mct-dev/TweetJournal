using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetJournal.Access.Entries.Domain;

namespace TweetJournal.Access.Entries
{
    [ExcludeFromCodeCoverage]
    public class EntryAccess : IEntryAccess
    {
        private readonly EntryContext _entryContext;

        public EntryAccess(EntryContext entryContext)
        {
            _entryContext = entryContext;
        }

        public async Task<Entry> GetByIdAsync(Guid id)
        {
            return await _entryContext.Entries.SingleOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<IEnumerable<Entry>> GetAsync()
        {
            return await _entryContext.Entries.ToListAsync();
        }
        
        public async Task<bool> CreateAsync(Entry newEntry)
        {
            await _entryContext.Entries.AddAsync(newEntry);
            var numberOfUpdates = await _entryContext.SaveChangesAsync();
            return numberOfUpdates > 0;
        }

        // should replace with Patch
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
