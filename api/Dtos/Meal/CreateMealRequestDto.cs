using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Meal
{
    public class CreateMealRequestDto
    {
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        
    }
}