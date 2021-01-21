using BugsFrontend.Interfaces;
using BugsFrontend.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace BugsFrontendTests
{
    public class BugsApiRequestMockData : IBugsApiRequest
    {
        public string BugsName;
        public HttpResponseMessage Result;
        private readonly HttpClient _httpClient = new HttpClient();
        public string JsonString = JsonConvert.SerializeObject(new List<BugModel> { new BugModel { Id = 1, Name = "no product" } });


        public async Task<List<BugModel>> GetBugsAsync()
        {
            try
            {
                Result = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonString)
                };

                var content = await Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BugModel>>(content);
            }
            catch (HttpRequestException e)
            {

                throw new HttpRequestException($"{e}");
            }
            catch (HttpResponseException)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);

            }
        }

        public async Task DeleteBugAsync(int id)
        {
            Result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonString)
            };

            if (!Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
        }

        public async Task UpdateBugAsync(int id, string name)
        {
            Result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonString)
            };
            if (!Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
        }

        public async Task CreateBugAsync(string name)
        {

            var bug = new BugModel { Name = name };
            BugsName = JsonConvert.SerializeObject(bug);
            Result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonString)
            };

            if (!Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
        }
    }
}
