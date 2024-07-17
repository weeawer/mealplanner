using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context)
            {
                _context = context;
            }

        public Task<bool> AppUserExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> CreateAsync(AppUser appUserModel)
        {
            await _context.AppUsers.AddAsync(appUserModel);
            await _context.SaveChangesAsync();
            return appUserModel;
        }

        public Task<Day?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}