using System.Linq;
using ITTWEB_Assignment6_FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITTWEB_Assignment6_FitnessApp.Controllers
{
    [Route("api/[controller]")]
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id}", Name = "GetExercise")]
        public IActionResult Get(long id) {
            
            var item = _context.Exercises.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] Exercise item) {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Exercises.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetExercise", new { id = item.Id }, item);
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] Exercise item) {
            if (item == null)
            {
                return BadRequest();
            }

            var exercise = _context.Exercises.FirstOrDefault(t => t.Id == item.Id);
            if (exercise == null)
            {
                return NotFound();
            }

            exercise.Name = item.Name;
            exercise.Description = item.Description;
            exercise.Sets = item.Sets;
            exercise.Reps = item.Reps;

            _context.Exercises.Update(exercise);
            _context.SaveChanges();

            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            
            var exercise = _context.Exercises.FirstOrDefault(t => t.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            _context.SaveChanges();
            
            return new NoContentResult();
        }
    }
}