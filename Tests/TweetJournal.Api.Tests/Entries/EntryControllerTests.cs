using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TweetJournal.Access.Entries;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Responses;
using TweetJournal.Api.Controllers.V1;
using TweetJournal.Api.Exceptions;

namespace TweetJournal.Api.Tests.Entries
{
    [TestFixture]
    public class EntryControllerTests
    {
        private MockWebContext _httpContext;
        private Mock<IMapper> _mapper;
        private Mock<IEntryAccess> _entryAccess;
        private EntryController _sut;

        [SetUp]
        public void Setup()
        {
            _httpContext = new MockWebContext();
            _mapper = new Mock<IMapper>(MockBehavior.Strict);
            _entryAccess = new Mock<IEntryAccess>();
            _sut = new EntryController(_entryAccess.Object, _mapper.Object)
            {
                ControllerContext = MockWebContext.BasicControllerContext()
            };

        }

        [TearDown]
        public void TearDown()
        {
            _mapper.VerifyAll();
            _entryAccess.VerifyAll();
        }

        [Test]
        public async Task ShouldCreateAnEntry()
        {
            var createEntryRequest = Mother.CreateEntryRequest;
            var contractEntry = Mother.ContractEntry;
            var entryResponse = Mother.EntryResponse;
            
            _mapper
                .Setup(m => m.Map<Entry>(createEntryRequest))
                .Returns(contractEntry);
            _mapper
                .Setup(m => m.Map<EntryResponse>(contractEntry))
                .Returns(entryResponse);
            _entryAccess
                .Setup(ea => ea.CreateAsync(contractEntry))
                .ReturnsAsync(true);

            var actual = await _sut.Create(createEntryRequest);
            var result = (CreatedResult)actual;
            
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(entryResponse, ((EntryResponse)result.Value));
        }

        [Test]
        public async Task ShouldGetAllEntries()
        {
            var entryList = Mother.GetTestEntriesList();
            var entryResponseList = Mother.GetTestEntryResponseList();
            
            _mapper
                .Setup(m => m.Map<IEnumerable<EntryResponse>>(entryList))
                .Returns(entryResponseList);
            _entryAccess
                .Setup(ea => ea.GetAsync())
                .ReturnsAsync(entryList);

            var actual = await _sut.GetAll();
            var result = (OkObjectResult)actual;
            
            Assert.AreEqual(result.Value, entryResponseList);
        }

        [Test]
        public void ShouldThrowNotFoundForNullGetEntriesData()
        {
            _entryAccess
                .Setup(ea => ea.GetAsync())
                .ReturnsAsync((IEnumerable<Entry>)null);

            Assert.ThrowsAsync<RecordNotFoundException>(() => _sut.GetAll());
        }
        
        [Test]
        public async Task ShouldGetEntryById()
        {
            Assert.Pass();
        }
        
        [Test]
        public async Task ShouldUpdateEntry()
        {
            Assert.Pass();
        }
        
        [Test]
        public async Task ShouldDeleteEntry()
        {
            Assert.Pass();
        }
    }
}