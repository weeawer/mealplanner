using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Choise
    {
        public int Id {get; set;}
        public int? AppUserId {get; set;}
         //один выбор много дней обэдов+челов - например у чела массив выборов
        public int? DayId {get; set;}
       
        public AppUser? AppUser {get; set;}
        //public Meal? Meal {get; set;}
        public Day? Day {get; set;}
        public List<Meal> Meals {get; set;} = new List<Meal>();


    }
}