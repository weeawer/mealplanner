using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser?>GetByIdAsync(string id); // FirstOfDefault CAN BE NULL
    }
}