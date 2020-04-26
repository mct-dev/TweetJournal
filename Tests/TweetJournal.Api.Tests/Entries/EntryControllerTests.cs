using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TweetJournal.Access.Entries;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
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
            
            Assert.AreEqual(result.StatusCode, 201);
            Assert.AreEqual(((EntryResponse)result.Value), entryResponse);
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
            
            Assert.AreEqual(entryResponseList, result.Value);
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
            var entry = Mother.ContractEntry;
            var entryResponse = Mother.EntryResponse;
            var entryId = Mother.TestEntryId;
            
            _mapper
                .Setup(m => m.Map<EntryResponse>(entry))
                .Returns(entryResponse);
            _entryAccess
                .Setup(ea => ea.GetByIdAsync(entryId))
                .ReturnsAsync(entry);

            var actual = await _sut.GetOne(entryId);
            var result = (OkObjectResult)actual;
            
            Assert.AreEqual(entryResponse, result.Value);
        }
        
        [Test]
        public async Task ShouldUpdateEntry()
        {
            var entryId = Mother.TestEntryId;
            var updateEntryRequest = new UpdateEntryRequest
            {
                Content = "Updated entry!"
            };
            var updatedEntry = new Entry
            {
                Id = entryId,
                Content = updateEntryRequest.Content,
            };
            var entryResponse = new EntryResponse
            {
                Id = entryId,
                Content = updateEntryRequest.Content
            };

            _mapper
                .Setup(m => m.Map<Entry>(updateEntryRequest))
                .Returns(updatedEntry);
            _mapper
                .Setup(m => m.Map<EntryResponse>(updatedEntry))
                .Returns(entryResponse);
            _entryAccess
                .Setup(ea => ea.UpdateAsync(updatedEntry))
                .ReturnsAsync(true);
            _entryAccess
                .Setup(ea => ea.GetByIdAsync(entryId))
                .ReturnsAsync(updatedEntry);

            var actual = await _sut.Update(entryId, updateEntryRequest);
            var result = (OkObjectResult) actual;
            
            Assert.AreEqual(entryResponse.Content, ((EntryResponse)result.Value).Content);
        }
        
        [Test]
        public async Task ShouldReturn201ForSuccessfulDeleteEntry()
        {
            var entryId = Mother.TestEntryId;
            var expected = new NoContentResult();
            
            _entryAccess
                .Setup(ea => ea.DeleteAsync(entryId))
                .ReturnsAsync(true);

            var actual = await _sut.Delete(entryId);
            var result = (NoContentResult) actual;
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
        }
        [Test]
        public async Task ShouldReturn404ForDeleteEntryNotFound()
        {
            var entryId = Mother.TestEntryId;
            var expected = new NotFoundResult();
            
            _entryAccess
                .Setup(ea => ea.DeleteAsync(entryId))
                .ReturnsAsync(false);

            var actual = await _sut.Delete(entryId);
            var result = (NotFoundResult) actual;
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
        }
        
    }
}