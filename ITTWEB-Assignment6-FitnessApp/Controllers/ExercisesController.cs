using System;
using System.Linq;
using ITTWEB_Assignment6_FitnessApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITTWEB_Assignment6_FitnessApp.Controllers
{
    [Produces("application/json")]
    [Route("api/exercises")]
    [Authorize]
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public JsonResult Get([FromRoute] long id)
        {
            Workout workout = _context.Workouts.Include(w => w.Exercises).First(w => w.Id == id);
            
            //return Json(_context.Exercises.ToList());
            return Json(workout.Exercises.ToList());
        }
        
        [HttpPost]
        public JsonResult Post([FromBody] Exercise dtoExercise)
        {
            var newExercise = new Exercise()
            {
                Name = dtoExercise.Name,
                Description = dtoExercise.Description,
                Reps = dtoExercise.Reps,
                Sets = dtoExercise.Sets
            };

            var dbExercise = _context.Exercises.Add(newExercise);
            _context.SaveChanges();
            return Json(dbExercise.Entity);
        }
        
        [HttpPut]
        public JsonResult Put([FromBody] Exercise dtoExercise) 
        {
            var dbExercise = _context.Exercises.Update(dtoExercise);
            _context.SaveChanges();
            return Json(dbExercise.Entity);
        }
        
        [HttpDelete("{id}")]
        public JsonResult Delete([FromRoute] long id)
        {
            var dbExercise = _context.Exercises.First(w => w.Id == id);
            var deletedExercise = _context.Exercises.Remove(dbExercise);
            _context.SaveChanges();
            return Json(deletedExercise.Entity);
        }
    }
}