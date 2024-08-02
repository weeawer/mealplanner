using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;

namespace api.Dtos.Meal
{
    public class MealDto
    {
        public int Id {get; set;}
        public int? CategoryId { get; set;}
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public int Price { get; set; }
        public int? DayId {get; set;}
        

        
        
        
        
        
    }
}