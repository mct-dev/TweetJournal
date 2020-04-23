using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using TweetJournal.Api.Mapping;

namespace TweetJournal.Api
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration ConfigureMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddMaps(GetMapperAssemblies());
            });

            return config;
        }

        public static IEnumerable<Assembly> GetMapperAssemblies()
        {
            yield return Assembly.GetExecutingAssembly();
        }
    }
}