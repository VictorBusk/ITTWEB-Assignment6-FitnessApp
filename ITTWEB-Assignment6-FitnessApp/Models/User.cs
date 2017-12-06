﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITTWEB_Assignment6_FitnessApp.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public List<Workout> Workouts { get; set; }
    }
}