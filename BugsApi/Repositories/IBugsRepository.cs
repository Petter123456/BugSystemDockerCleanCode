using BugsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugsApi.Repositories
{
    public interface IBugsRepository
    {
        Task<ActionResult<IEnumerable<BugModel>>> Get();
        Task<IActionResult> Update(int id, BugModel bug);
        Task<IActionResult> Create(BugModel bugModel);
        Task<IActionResult> Delete(int id); 
    }
}