using BugsApi.Data;
using BugsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugController : Controller
    {
        private readonly AppDbContext _db;

        public BugController(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BugModel>>> Get()
        {
         
            return await _db.Bug.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bug = await _db.Bug.FirstOrDefaultAsync(n => n.Id == id);

            return new JsonResult(bug);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(BugModel bug)
        {
            await _db.Bug.AddAsync(bug);
            await _db.SaveChangesAsync();

            return new JsonResult(bug.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, BugModel bug)
        {
            var existingBug = await _db.Bug.FirstOrDefaultAsync(n => n.Id == id);
            existingBug.Name = bug.Name;
            var success = (await _db.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bug = await _db.Bug.FirstOrDefaultAsync(n => n.Id == id);
            _db.Remove(bug);
            var success = (await _db.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }
    }
}
