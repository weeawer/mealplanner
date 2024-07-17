using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Meal
    {
        public int Id {get; set;}

        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        
        
        public int? DayId {get; set;}
        //Navigation Property
      
        public Day? Day {get; set;}
        public List<Choise> Choises {get; set;} = new List<Choise>();// не показываем но храним
    }
}