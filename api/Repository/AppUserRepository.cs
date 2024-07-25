using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context)
            {
                _context = context;
            }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await _context.AppUsers
                             .Include(u => u.ChoiseMeals)
                             .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}