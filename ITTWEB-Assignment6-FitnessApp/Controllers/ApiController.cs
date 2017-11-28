using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ITTWEB_Assignment6_FitnessApp.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "Hello", "World" };
        }
    }
}