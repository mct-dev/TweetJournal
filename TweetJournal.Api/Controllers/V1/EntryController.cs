using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TweetJournal.Access.Entries;
using TweetJournal.Access.Entries.Domain;
using TweetJournal.Api.Contracts.V1;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;
using TweetJournal.Api.Domain;
using TweetJournal.Api.Exceptions;

namespace TweetJournal.Api.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntryController : ControllerBase
    {
        private readonly IEntryAccess _entryAccess;
        private readonly IMapper _mapper;
        
        public EntryController(IEntryAccess entryAccess, IMapper mapper)
        {
            _entryAccess = entryAccess;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Entry.GetAll)]
        [SwaggerResponse(200)]
        public async Task<ActionResult> GetAll()
        {
            var entries = await _entryAccess.GetAsync();
            if (entries == null)
                throw new RecordNotFoundException("Unable to find entry records.");

            var entriesResponse = _mapper.Map<IEnumerable<EntryResponse>>(entries);
            return Ok(entriesResponse);
        }

        [HttpGet(ApiRoutes.Entry.GetOne)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]
        public async Task<ActionResult> GetOne([FromRoute] Guid entryId)
        {
            var entry = await _entryAccess.GetByIdAsync(entryId);
            if (entry == null)
            {
                return NotFound();
            }

            var entryResponse = _mapper.Map<EntryResponse>(entry);
            return Ok(entryResponse);
        }
        

        [HttpPatch(ApiRoutes.Entry.Patch)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]
        public async Task<ActionResult> Patch([FromRoute] Guid entryId, [FromBody] JsonPatchDocument<UpdateEntryRequest> updateEntryRequest)
        {
            var existingEntry = await _entryAccess.GetByIdAsync(entryId);
            if (existingEntry == null)
            {
                throw new RecordNotFoundException($"Unable to find entry with id: {entryId}.");
            }
            
            var patchedEntry = _mapper.Map<HydratedUpdateEntryRequest>(existingEntry);
            updateEntryRequest.ApplyTo(patchedEntry);
            
            var entryWithUpdates = _mapper.Map<Entry>(patchedEntry);
            await _entryAccess.UpdateAsync(entryWithUpdates);

            var updatedEntryResponse = _mapper.Map<EntryResponse>(entryWithUpdates);

            return Ok(updatedEntryResponse);
        }

        [HttpDelete(ApiRoutes.Entry.Delete)]
        [SwaggerResponse(404, "Item with provided id was not found.")]
        [SwaggerResponse(204, "Item was deleted.")]
        public async Task<ActionResult> Delete([FromRoute] Guid entryId)
        {
            var successful = await _entryAccess.DeleteAsync(entryId);
            if (!successful)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost(ApiRoutes.Entry.Create)]
        [SwaggerResponse(201, "Item was created.")]
        public async Task<ActionResult> Create([FromBody] CreateEntryRequest createEntryRequest)
        {
            var entry = _mapper.Map<Entry>(createEntryRequest);

            await _entryAccess.CreateAsync(entry);

            var entryResponse = _mapper.Map<EntryResponse>(entry);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Entry.GetOne.Replace("{entryId}", entry.Id.ToString());

            return Created(locationUri, entryResponse);
        }
    }
}