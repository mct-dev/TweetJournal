using AutoMapper;
using NUnit.Framework;

namespace TweetJournal.Api.Tests.Mapping
{
    public class MappingTests
    {
        private IMapper _sut;
        
        [SetUp]
        public void Setup()
        {
            _sut = Mother.ConfigureAutoMapper();
        }

        [Test]
        public void ShouldValidateAutoMapperConfiguration()
        {
            var config = Mother.GetAutoMapperConfiguration();
            config.AssertConfigurationIsValid();
        }
    }
}