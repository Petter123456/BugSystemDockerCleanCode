using BugsApi.Data;
using BugsApi.Models;
using BugsApi.Repositories;
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
        private readonly IBugsRepository _bugsRepository;

        public BugController(IBugsRepository bugsRepository)
        {
            _bugsRepository = bugsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BugModel>>> Get()
        {
           return await _bugsRepository.Get(); 
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(BugModel bug)
        {
           return await _bugsRepository.Create(bug); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, BugModel bug)
        {
            return await _bugsRepository.Update(id, bug); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _bugsRepository.Delete(id); 
        }
    }
}
