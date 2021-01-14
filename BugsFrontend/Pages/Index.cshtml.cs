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
        public IndexModel()
        {

        }

        public async Task OnGet()
        {
                ViewData["Message"] = "Welcome to the bug system";
        }
    }
}
