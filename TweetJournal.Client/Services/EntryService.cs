using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TweetJournal.Client.Models;

namespace TweetJournal.Client.Services
{
    public class EntryService : IEntryService
    {
        private HttpClient _httpClient { get; set; }

        public EntryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5555");
        }

        public async Task<Response<IEnumerable<Entry>>> GetAllAsync()
        {
            var httpResponse = await _httpClient.GetAsync("/api/v1/entry");
            var result = await httpResponse.Content.ReadAsStringAsync();
            var isError = result.Contains("\"error\"");

            return ParseEntryResponse<IEnumerable<Entry>>(result, isError);
        }

        private Response<T> ParseEntryResponse<T>(string result, bool isError)
        {
            var response = new Response<T> { IsError = isError };
            try
            {
                if (isError)
                {
                    response.Error = JsonConvert.DeserializeObject<Error>(result).Message;
                }
                else
                {
                    response.Result = JsonConvert.DeserializeObject<T>(result);
                }
            }
            catch (Exception e)
            {
                // TODO: logger.log(e)
                response.IsError = true;
                response.Error = $"Error while deserializing Entry response: {result}";
            }

            return response;
        }
    }
}
