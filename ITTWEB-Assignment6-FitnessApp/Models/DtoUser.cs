using System.Collections.Generic;

namespace ITTWEB_Assignment6_FitnessApp.Models
{
    public class DtoUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Workout> Workouts { get; set; }
    }
}