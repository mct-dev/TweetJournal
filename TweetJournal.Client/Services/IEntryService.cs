using System.Collections.Generic;
using System.Threading.Tasks;
using TweetJournal.Client.Models;

namespace TweetJournal.Client.Services
{
    public interface IEntryService
    {
        Task<Response<IEnumerable<Entry>>> GetAllAsync();

    }
}
