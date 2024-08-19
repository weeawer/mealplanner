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
        public int Shift {get; set;}
        public int EmpType {get; set;}
        public List<ChoiseMealsSwap> ChoiseMeals { get; set; } = new List<ChoiseMealsSwap>();

    }
}