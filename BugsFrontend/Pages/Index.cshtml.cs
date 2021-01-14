using BugsFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BugsFrontend.Pages
{
    public class IndexModel : PageModel
    {
        private string bugController = "Bug/";
        private string bugsName;
        private HttpResponseMessage result;
        private List<BugModel> bug;
        private readonly HttpClient httpClient;


        public IndexModel(IConfiguration config, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(config.GetValue<string>("BugClientUrl"));
        }

        public async Task OnGet()
        {
                ViewData["Message"] = "Welcome to the bug system";

                result = await httpClient.GetAsync(bugController);
                var content = await result.Content.ReadAsStringAsync();
                bug = JsonConvert.DeserializeObject<List<BugModel>>(content);

                ViewData["bugs"] = bug; 

                ViewData["Message"] += " and " + bug[0].Name;
        }

        public async Task<IActionResult> OnGetDelete(string id)
        {           
            result = await httpClient.DeleteAsync(bugController + id);
            
            return Page(); 
        }

        public async Task OnPost(int id, string name)
        {
            var bug = new BugModel { Id = id, Name = name };
            bugsName = JsonConvert.SerializeObject(bug);
            result = await httpClient.PutAsync(bugController + id, new StringContent(bugsName.ToString(), Encoding.UTF8, "application/json"));

            RedirectToPage("/Index");
        }

        public async Task OnPostCreate(string name)
        {
            var bug = new BugModel { Name = name };
            bugsName = JsonConvert.SerializeObject(bug);
            result = await httpClient.PostAsync(bugController, new StringContent(bugsName.ToString(), Encoding.UTF8, "application/json"));

            await OnGet(); 
        }
    }
}
