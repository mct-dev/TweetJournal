using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TweetJournalApi.Contracts.V1;
using TweetJournalApi.Contracts.V1.Requests;
using TweetJournalApi.Contracts.V1.Responses;
using TweetJournalApi.Domain;
using TweetJournalApi.Services;

namespace TweetJournalApi.Controllers.V1
{
    [ApiController]
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
            var posts = await _entryService.GetAsync();
            var postsResponse = posts.Select(p => new EntryResponse
            {
                Id = p.Id,
                Content = p.Content,
                ModifiedDate = p.ModifiedDate,
                CreatedDate = p.CreatedDate
            });

            return Ok(postsResponse);
        }

        [HttpGet(ApiRoutes.Entry.GetOne)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]
        public async Task<ActionResult<EntryResponse>> GetOne([FromRoute] Guid postId)
        {
            var post = await _entryService.GetByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var postResponse = new EntryResponse
            {
                Id = post.Id,
                Content = post.Content,
                CreatedDate = post.CreatedDate,
                ModifiedDate = post.ModifiedDate
            };

            return Ok(postResponse);
        }

        [HttpPut(ApiRoutes.Entry.Update)]
        [SwaggerResponse(404)]
        [SwaggerResponse(200)]
        public async Task<ActionResult<EntryResponse>> Update([FromRoute] Guid postId, [FromBody] UpdateEntryRequest request)
        {
            var updatedPost = new Entry
            {
                Id = postId,
                Content = request.Content,
                ModifiedDate = DateTime.Now
            };

            var successful = await _entryService.UpdateAsync(updatedPost);
            if (!successful)
            {
                return NotFound();
            }

            var postResponse = new EntryResponse
            {
                Id = updatedPost.Id,
                Content = updatedPost.Content,
                CreatedDate = updatedPost.CreatedDate,
                ModifiedDate = updatedPost.ModifiedDate
            };

            return Ok(postResponse);
        }

        [HttpDelete(ApiRoutes.Entry.Delete)]
        [SwaggerResponse(404, "Item with provided id was not found.")]
        [SwaggerResponse(204, "Item was deleted.")]
        public async Task<ActionResult> Delete([FromRoute] Guid postId)
        {
            var successful = await _entryService.DeleteAsync(postId);

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

            var wasCreated = await _entryService.CreateAsync(entry);

            if (!wasCreated)
            {
                throw new Exception("Problem! Entry was not created!");
            }

            var postResponse = new EntryResponse { Id = entry.Id, Content = entry.Content, CreatedDate = entry.CreatedDate };
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Entry.GetOne.Replace("{postId}", entry.Id.ToString());

            return Created(locationUri, postResponse);
        }
    }
}