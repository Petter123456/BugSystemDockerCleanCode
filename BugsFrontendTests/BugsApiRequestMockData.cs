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
        private string bugsName;
        private HttpResponseMessage result;
        private readonly HttpClient httpClient = new HttpClient();
        private string bugController = "Bug/";
        public string jsonString = JsonConvert.SerializeObject(new List<BugModel> { new BugModel { Id = 1, Name = "no product" }
});


        public async Task<List<BugModel>> GetBugsAsync()
        {
            try
            {
                result = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                };

                var content = await result.Content.ReadAsStringAsync();
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

        public Task DeleteBugAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateBugAsync(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateBugAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
