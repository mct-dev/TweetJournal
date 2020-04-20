﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TweetJournal.Api.Domain;
using TweetJournal.Api.Services;
using TweetJournal.Contracts.V1;
using TweetJournal.Contracts.V1.Requests;
using TweetJournal.Contracts.V1.Responses;

namespace TweetJournal.Api.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntryController : ControllerBase
    {
        private readonly EntryService _entryService;
        public EntryController(EntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet(ApiRoutes.Entry.GetAll)]
        [SwaggerResponse(200)]
        public async Task<ActionResult<List<EntryResponse>>> GetAll()
        {
            var entries = await _entryService.GetAsync();
            var entriesResponse = entries.Select(p => new EntryResponse
            {
                Id = p.Id,
                Content = p.Content,
                ModifiedDate = p.ModifiedDate,
                CreatedDate = p.CreatedDate
            });

            return Ok(entriesResponse);
        }

        [HttpGet(ApiRoutes.Entry.GetOne)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]
        public async Task<ActionResult<EntryResponse>> GetOne([FromRoute] Guid entryId)
        {
            var entry = await _entryService.GetByIdAsync(entryId);

            if (entry == null)
            {
                return NotFound();
            }

            var entryResponse = new EntryResponse
            {
                Id = entry.Id,
                Content = entry.Content,
                CreatedDate = entry.CreatedDate,
                ModifiedDate = entry.ModifiedDate
            };

            return Ok(entryResponse);
        }

        [HttpPut(ApiRoutes.Entry.Update)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]
        public async Task<ActionResult<EntryResponse>> Update([FromRoute] Guid entryId, [FromBody] UpdateEntryRequest entryRequest)
        {
            var updatedEntry = new Entry
            {
                Id = entryId,
                Content = entryRequest.Content,
                ModifiedDate = DateTime.Now
            };

            var successful = await _entryService.UpdateAsync(updatedEntry);
            if (!successful)
            {
                return NotFound();
            }

            var entryResponse = new EntryResponse
            {
                Id = updatedEntry.Id,
                Content = updatedEntry.Content,
                CreatedDate = updatedEntry.CreatedDate,
                ModifiedDate = updatedEntry.ModifiedDate
            };

            return Ok(entryResponse);
        }

        [HttpDelete(ApiRoutes.Entry.Delete)]
        [SwaggerResponse(404, "Item with provided id was not found.")]
        [SwaggerResponse(204, "Item was deleted.")]
        public async Task<ActionResult> Delete([FromRoute] Guid entryId)
        {
            var successful = await _entryService.DeleteAsync(entryId);

            if (!successful)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost(ApiRoutes.Entry.Create)]
        [SwaggerResponse(201, "Item was created.")]
        public async Task<ActionResult<EntryResponse>> Create([FromBody] CreateEntryRequest entryRequest)
        {
            var entry = new Entry
            {
                Content = entryRequest.Content,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            await _entryService.CreateAsync(entry);

            var postResponse = new EntryResponse { Id = entry.Id, Content = entry.Content, CreatedDate = entry.CreatedDate };
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Entry.GetOne.Replace("{entryId}", entry.Id.ToString());

            return Created(locationUri, postResponse);
        }
    }
}