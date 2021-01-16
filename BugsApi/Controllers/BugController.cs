using BugsApi.Models;
using BugsApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugController : Controller
    {
        private readonly IBugsRepository _bugsRepository;

        public BugController(IBugsRepository bugsRepository)
        {
            _bugsRepository = bugsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BugModel>>> Get()
        {
            try
            {
                return await _bugsRepository.Get();
            }
            catch (Exception)
            {
                throw new Exception("Not able to retrive items from db");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(BugModel bug)
        {
            try
            {
                return await _bugsRepository.Create(bug);
            }
            catch (Exception)
            {

                throw new Exception("Not able to add bug to db");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, BugModel bug)
        {
            try
            {
                return await _bugsRepository.Update(id, bug);
            }
            catch (Exception)
            {

                throw new Exception("Not able to update bug in db");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _bugsRepository.Delete(id);
            }
            catch (Exception)
            {
                throw new Exception("Not able to delete bug in db");
            }
        }
    }
}
