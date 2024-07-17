using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Meal;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext _context;
        public MealRepository(ApplicationDbContext context)
            {
                _context = context;
            }

          

        public async Task<Meal> CreateAsync(Meal mealModel)
        {
            await _context.Meals.AddAsync(mealModel);
            await _context.SaveChangesAsync();
            return mealModel;    
        }

        public Task CreateAsync(object mealModel)
        {
            throw new NotImplementedException();
        }

     public async Task<bool> MealExists(int mealId)
{
    return await _context.Meals.AnyAsync(m => m.Id == mealId);
}

public async Task AddMealToDayAsync(Meal mealModel)
{
    _context.Meals.Add(mealModel);
    await _context.SaveChangesAsync();
}


        public async Task<Meal?> DeleteAsync(int id)
        {
            var mealModel = await _context.Meals.FirstOrDefaultAsync(x => x.Id == id);
            if (mealModel == null)
            {
                return null;
            }
            _context.Meals.Remove(mealModel);
            await _context.SaveChangesAsync();
            return mealModel;

        }

        public async Task<List<Meal>> GetAllAsync()
        {
            return await _context.Meals.ToListAsync();
        }

        public async Task<Meal?> GetByIdAsync(int id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task<Meal?> UpdateAsync(int id, UpdateMealRequestDto mealDto)
        {
            var existingMeal = await _context.Meals.FirstOrDefaultAsync(x=> x.Id == id);
            if(existingMeal == null )
            {
                return null;
            }
            existingMeal.Name = mealDto.Name;
            existingMeal.Description = mealDto.Description;

            await _context.SaveChangesAsync();
            return existingMeal;

        }
    }
}