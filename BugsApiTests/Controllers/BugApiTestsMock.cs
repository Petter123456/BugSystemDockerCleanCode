using BugsApi.Models;
using BugsApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugsApi.Controllers.Tests
{
    [TestClass]
    public class BugApiTestsMock : IBugsRepository
    {

        public async Task<IActionResult> Create(BugModel bugModel)
        {
            var bugs = new List<BugModel> { new BugModel { Id = 1, Name = "no product" }, new BugModel { Id = 2, Name = "no image" } };

            if (bugModel != null)
            {
                bugs.Add(bugModel);
                return new JsonResult(true);
            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bugs = new List<BugModel> { new BugModel { Id = 1, Name = "no product" }, new BugModel { Id = 2, Name = "no image" } };

            var existingBug = bugs.Find(x => x.Id == id);

            if (existingBug != null)
            {
                bugs.Remove(existingBug);
                return new JsonResult(true);

            } else
            {
                return null;
            }
        }

        public async Task<ActionResult<IEnumerable<BugModel>>> Get()
        {
            var bugs = new List<BugModel> { new BugModel { Id = 1, Name = "no product" } };

            return new JsonResult(bugs); 
        }

        public async Task<IActionResult> Update(int id, BugModel bug)
        {
            var bugs = new List<BugModel> { new BugModel { Id = 1, Name = "no product" } };

            var existingBug = bugs.Find(x => x.Id == id);
            if (existingBug != null)
            {
                existingBug = bug;
                return new JsonResult(true);
            } else
            {
                return null; 
            }
        }
    }
}