using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Day
    {
        public int Id {get; set;}
        public List<ChoiseMeal> ChoiseMeals {get; set;} = new List<ChoiseMeal>();
        public List<Meal> Meals {get; set;} = new List<Meal>();
    }
}