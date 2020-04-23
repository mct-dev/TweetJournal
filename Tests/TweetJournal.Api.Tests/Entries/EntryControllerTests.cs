using AutoMapper;
using Moq;
using NUnit.Framework;
using TweetJournal.Access.Entries;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Controllers.V1;

namespace TweetJournal.Api.Tests.Entries
{
    [TestFixture]
    public class EntryControllerTests
    {
        private EntryController _sut;
        private Mock<IMapper> _mapper;
        private Mock<IEntryAccess> _entryAccess;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>(MockBehavior.Strict);
            _entryAccess = new Mock<IEntryAccess>();
            _sut = new EntryController(_entryAccess.Object, _mapper.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mapper.VerifyAll();
            _entryAccess.VerifyAll();
        }

        [Test]
        public void ShouldCreateAnEntry()
        {
            var createEntryRequest = Mother.TestCreateEntryRequest;
            var contractEntry = Mother.ContractEntry;
            
            _mapper.Setup(m => m.Map<Entry>(createEntryRequest)).Returns(contractEntry);
        }
    }
}