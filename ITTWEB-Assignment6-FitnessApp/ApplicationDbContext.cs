using ITTWEB_Assignment6_FitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ITTWEB_Assignment6_FitnessApp
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}