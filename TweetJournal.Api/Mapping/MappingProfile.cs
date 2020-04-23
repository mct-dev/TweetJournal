using AutoMapper;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;

namespace TweetJournal.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<CreateEntryRequest, Entry>();
            CreateMap<Entry, EntryResponse>();
        }
    }
}