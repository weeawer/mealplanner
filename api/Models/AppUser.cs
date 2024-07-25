using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser: IdentityUser
    {
        public string Id {get; set;}
        public List<ChoiseMeal> ChoiseMeals { get; set; } = new List<ChoiseMeal>();

    }
}