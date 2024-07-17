using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser: IdentityUser
    {
        public int AppUserId {get; set;}
        public string AppUserName {get;set;} = string.Empty;
        public List<Choise> Choises {get; set;} = new List<Choise>();
        
    }
}