using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITTWEB_Assignment6_FitnessApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITTWEB_Assignment6_FitnessApp.Controllers
{
    [Produces("application/json")]
    [Route("api/workouts")]
    [Authorize]
    public class WorkoutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_context.Workouts.ToList());
        }
        
        [HttpPost]
        public JsonResult Post([FromBody] Workout dtoWorkout)
        {
            var newWorkout = new Workout()
            {
                Name = dtoWorkout.Name,
                Description = dtoWorkout.Description
            };

            var dbWorkout = _context.Workouts.Add(newWorkout);
            _context.SaveChanges();
            return Json(dbWorkout.Entity);
        }
        
        [HttpPut]
        public JsonResult Put([FromBody] Workout dtoWorkout) 
        {
            var dbWorkout = _context.Workouts.Update(dtoWorkout);
            _context.SaveChanges();
            return Json(dbWorkout.Entity);
        }
        
        [HttpDelete("{id}")]
        public JsonResult Delete([FromRoute] long id)
        {
            var dbWorkout = _context.Workouts.First(w => w.Id == id);
            var deletedWorkout = _context.Workouts.Remove(dbWorkout);
            _context.SaveChanges();
            return Json(deletedWorkout.Entity);
        }
    }
}