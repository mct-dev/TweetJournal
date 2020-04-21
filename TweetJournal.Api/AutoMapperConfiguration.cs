using System.Collections.Generic;
using AutoMapper;

namespace TweetJournal.Api
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfiles(GetMapperAssemblies());
            });
        }

        private static IEnumerable<Profile> GetMapperAssemblies()
        {
            yield return null;
        }
    }
}