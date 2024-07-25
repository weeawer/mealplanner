using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Meal;

namespace api.Dtos.Day
{
    public class DayDto
    {
        public int Id {get; set;} 
        public List<MealDto> Meals {get; set;}   
    }
}