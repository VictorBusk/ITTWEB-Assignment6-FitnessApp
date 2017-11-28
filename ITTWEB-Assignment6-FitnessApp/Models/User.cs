using System.Collections.Generic;

namespace ITTWEB_Assignment6_FitnessApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Workout> Workouts { get; set; }
    }
}