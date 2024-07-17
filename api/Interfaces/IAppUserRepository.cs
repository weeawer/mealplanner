using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetAllAsync();

        Task<AppUser?>GetByIdAsync(int id); // FirstOfDefault CAN BE NULL
        Task<AppUser> CreateAsync (AppUser appUserModel);   
    //    Task<Day?> UpdateAsync (int id, UpdateAppUserRequestDto appUserRequestDto);
        Task<Day?> DeleteAsync(int id);
        Task<bool> AppUserExists(int id);
    }
}