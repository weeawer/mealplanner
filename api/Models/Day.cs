using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Day
    {
        public int Id {get; set;}
        public DateTime DayDate {get; set;} = DateTime.Now; 
        
   //30 06 //  //  // public int? UserId {get; set;}
        //Navigation Property
        //public User? User {get; set;}

        //
        public List<Meal> Meals {get; set;} = new List<Meal>();
        public List<Choise> Choises {get; set;} = new List<Choise>(); // не показываем но храним
    }
}