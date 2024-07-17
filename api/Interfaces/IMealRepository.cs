using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Meal;
using api.Models;

namespace api.Interfaces
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync(); 
        Task<Meal?>GetByIdAsync(int id); // FirstOfDefault CAN BE NULL
        Task<Meal> CreateAsync (Meal mealModel); 
       
       Task AddMealToDayAsync(Meal mealModel);
       Task<bool> MealExists(int mealId);


        Task<Meal?> UpdateAsync (int id, UpdateMealRequestDto mealDto);  //, UpdateDayRequestDto dayDto);
        Task<Meal?> DeleteAsync(int id);
        Task CreateAsync(object mealModel);
    }
}