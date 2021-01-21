using BugsApi.Data;
using BugsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugsApi.Repositories
{
    public class BugsRepository : IBugsRepository
    {
        private readonly AppDbContext appDbContext;

        public BugsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IActionResult> Create(BugModel bugModel)
        {
            await appDbContext.Bug.AddAsync(bugModel);
            await appDbContext.SaveChangesAsync();

            return new JsonResult(bugModel.Id);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bug = await appDbContext.Bug.FirstOrDefaultAsync(n => n.Id == id);
            appDbContext.Remove(bug);
            var success = (await appDbContext.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }

        public async Task<ActionResult<IEnumerable<BugModel>>> Get()
        {
            return await appDbContext.Bug.ToListAsync();
        }

        public async Task<IActionResult> Update(int id, BugModel bug)
        {
            var existingBug = await appDbContext.Bug.FirstOrDefaultAsync(n => n.Id == id);
            existingBug.Name = bug.Name;
            var success = (await appDbContext.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }
    }
}
