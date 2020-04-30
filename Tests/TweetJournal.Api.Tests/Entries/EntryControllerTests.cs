using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TweetJournal.Access.Entries;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;
using TweetJournal.Api.Controllers.V1;
using TweetJournal.Api.Domain;
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
            var createEntryRequest = Mother.GenericCreateEntryRequest;
            var contractEntry = Mother.GenericEntry;
            var entryResponse = Mother.GenericEntryResponse;
            
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
            var entry = Mother.GenericEntry;
            var entryResponse = Mother.GenericEntryResponse;
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
        public async Task ShouldPatchEntry()
        {
            var initialEntry = Mother.GenericEntry;

            var updateEntryRequest = Mother.UpdateEntryRequest;
            var hydratedEntryRequest = Mother.HydratedUpdateEntryRequest;
            var updatedEntry = Mother.UpdatedEntry;
            var updatedEntryResponse = Mother.UpdatedEntryResponse;
            var patchDocument = new JsonPatchDocument<UpdateEntryRequest>();
            
            patchDocument.Add(e => e.Content, updatedEntry.Content);

            _mapper
                .Setup(m => m.Map<HydratedUpdateEntryRequest>(initialEntry))
                .Returns(hydratedEntryRequest);
            _mapper
                .Setup(m => m.Map<Entry>(hydratedEntryRequest))
                .Returns(updatedEntry);
            _mapper
                .Setup(m => m.Map<EntryResponse>(updatedEntry))
                .Returns(updatedEntryResponse);

            _entryAccess
                .Setup(ea => ea.GetByIdAsync(initialEntry.Id))
                .ReturnsAsync(initialEntry);
            _entryAccess
                .Setup(ea => ea.UpdateAsync(updatedEntry))
                .ReturnsAsync(true);

            var actual = await _sut.Patch(initialEntry.Id, patchDocument);
            var result = (OkObjectResult) actual;
            
            Assert.AreEqual(updateEntryRequest.Content, ((EntryResponse)result.Value).Content);
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