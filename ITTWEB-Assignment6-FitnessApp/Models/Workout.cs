using System.Collections.Generic;

namespace ITTWEB_Assignment6_FitnessApp.Models
{
    public class Workout
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}