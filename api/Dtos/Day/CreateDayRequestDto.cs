using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Meal;

namespace api.Dtos.Day
{
    public class CreateDayRequestDto
    {        
        public List<MealDto> Meals {get; set;} 
    }
}