using BugsFrontend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BugsFrontend.Pages
{
    public class BugViewModel : PageModel
    {
        private readonly IBugsApiRequest _bugsApiRequest;

        public BugViewModel(IBugsApiRequest bugsApiRequest)
        {
            _bugsApiRequest = bugsApiRequest;
        }

        public async Task OnGet()
        {
            ViewData["Message"] = "Welcome to the bug system";

            var bugs = await _bugsApiRequest.GetBugsAsync();

            ViewData["bugs"] = bugs;
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            await _bugsApiRequest.DeleteBugAsync(id);
            return RedirectToPage("Bugs");
        }

        public async Task<IActionResult> OnPost(int id, string name)
        {
            await _bugsApiRequest.UpdateBugAsync(id, name);

            return RedirectToPage("Bugs");
        }

        public async Task<IActionResult> OnPostCreate(string name)
        {
            await _bugsApiRequest.CreateBugAsync(name);

            return RedirectToPage("Bugs");
        }
    }
}
