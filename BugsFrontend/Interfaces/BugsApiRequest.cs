using BugsFrontend.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

namespace BugsFrontend.Interfaces
{
    public class BugsApiRequest : IBugsApiRequest
    {
        public const string BugController = "Bug/";
        public string BugsName;
        public HttpResponseMessage Result;
        public HttpClient HttpClient;
        public BugsApiRequest(IConfiguration config, HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(config.GetValue<string>("BugClientUrl"));
        }

        public async Task<List<BugModel>> GetBugsAsync()
        {
            try
            {
                Result = await HttpClient.GetAsync(BugController);
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
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task DeleteBugAsync(int id)
        {
            Result = await HttpClient.DeleteAsync(BugController + id);

            if (!Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
        }

        public async Task UpdateBugAsync(int id, string name)
        {
            var bug = new BugModel { Id = id, Name = name };
            BugsName = JsonConvert.SerializeObject(bug);
            Result = await HttpClient.PutAsync(BugController + id, new StringContent(BugsName.ToString(), Encoding.UTF8, "application/json"));

            if (!Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
        }

        public async Task CreateBugAsync(string name)
        {
            var bug = new BugModel { Name = name };
            BugsName = JsonConvert.SerializeObject(bug);
            Result = await HttpClient.PostAsync(BugController, new StringContent(BugsName.ToString(), Encoding.UTF8, "application/json"));

            if (!Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
        }
    }
}
