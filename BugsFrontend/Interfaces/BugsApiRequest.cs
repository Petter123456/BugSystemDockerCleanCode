using BugsFrontend.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

namespace BugsFrontend.Interfaces
{
    public class BugsApiRequest : IBugsApiRequest
    {
        public string bugController = "Bug/";
        public string bugsName;
        public HttpResponseMessage result;
        public HttpClient httpClient;
        public BugsApiRequest(IConfiguration config, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(config.GetValue<string>("BugClientUrl"));
        }

        public async Task<List<BugModel>> GetBugsAsync()
        {
            try
            {
                result = await httpClient.GetAsync(bugController);
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BugModel>>(content);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e}");
            }
            catch(HttpResponseException)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);

            }

        }

        public async Task DeleteBugAsync(int id)
        {
            try
            {
                result = await httpClient.DeleteAsync(bugController + id);
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

        public async Task UpdateBugAsync(int id, string name)
        {
            try
            {
                var bug = new BugModel { Id = id, Name = name };
                bugsName = JsonConvert.SerializeObject(bug);
                result = await httpClient.PutAsync(bugController + id, new StringContent(bugsName.ToString(), Encoding.UTF8, "application/json"));
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

        public async Task CreateBugAsync(string name)
        {
            try
            {
                var bug = new BugModel { Name = name };
                bugsName = JsonConvert.SerializeObject(bug);
                result = await httpClient.PostAsync(bugController, new StringContent(bugsName.ToString(), Encoding.UTF8, "application/json"));
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
    }
}
