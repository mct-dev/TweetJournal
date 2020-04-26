using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;

namespace TweetJournal.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEntryRequest, Entry>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModifiedDate, opts => opts.MapFrom(src => DateTime.Now));
            CreateMap<Entry, EntryResponse>();
            CreateMap<UpdateEntryRequest, Entry>()
                .ForMember(dest => dest.ModifiedDate, opts => opts.MapFrom(src => DateTime.Now));
        }
    }
}