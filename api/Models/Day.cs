using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Day
    {
        public int Id {get; set;}
        public DateTime Date { get; set; }
        public List<ChoiseMealsSwap> ChoiseMeals {get; set;} = new List<ChoiseMealsSwap>();
        public List<Meal> Meals {get; set;} = new List<Meal>();
    }
}