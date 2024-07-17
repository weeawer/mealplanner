using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Meal
{
    public class CreateMealWithIdsDto
    {
        public List<int> MealsIds { get; set; } = new List<int>();  
    }
}