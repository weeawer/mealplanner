using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Day;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Repository.Bases
{
    public class DayRepository : IDayRepository

    {

        private readonly ApplicationDbContext _context;
        public DayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Day> CreateAsync(Day dayModel)
        {
            await _context.Days.AddAsync(dayModel);
            await _context.SaveChangesAsync();
            return dayModel;
        }

        public Task<bool> DayExists(int id)
        {
            return _context.Days.AnyAsync(s => s.Id == id);
        }

        public async Task<Day?> DeleteAsync(int id)
        {
            var dayModel = await _context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (dayModel == null)
            {
                return null;
            }
            _context.Days.Remove(dayModel);
            await _context.SaveChangesAsync();
            return dayModel;

        }

        public async Task<List<Day>> GetAllAsync()
        {
            return await _context.Days.Include(c => c.Meals).ToListAsync();
        }

        public async Task<Day?> GetByIdAsync(int id)
        {
            return await _context.Days.Include(c => c.Meals).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Day?> UpdateAsync(int id, UpdateDayRequestDto dayDto)
        {
            var existingDay = await _context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDay == null)
            {
                return null;
            }


            await _context.SaveChangesAsync();
            return existingDay;

        }
    }
};