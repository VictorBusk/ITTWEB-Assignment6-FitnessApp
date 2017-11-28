using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ITTWEB_Assignment6_FitnessApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "Hello", "World" };
        }
        
        [HttpPost]
        public IEnumerable<string> Post() {
            return new string[] { "Hello", "World" };
        }
        
        [HttpPut]
        public IEnumerable<string> Put() {
            return new string[] { "Hello", "World" };
        }
        
        [HttpDelete("{id}")]
        public IEnumerable<string> Delete() {
            return new string[] { "Hello", "World" };
        }
    }
}