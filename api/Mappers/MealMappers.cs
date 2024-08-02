using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Meal;
using api.Models;

namespace api.Mappers
{
    public static class MealMappers
    {
        public static MealDto ToMealDto(this Meal mealModel)
        {
            return new MealDto
            {
                Id = mealModel.Id,
                CategoryId = mealModel.CategoryId,
                Name = mealModel.Name, 
                Description = mealModel.Description,
                Price = mealModel.Price,
                DayId = mealModel.DayId,
                
                
            };
        }
        public static Meal ToMealFromCreate(this CreateMealRequestDto mealDto, int dayId)
        {
            return new Meal
            {
                
                Name = mealDto.Name, 
                Description = mealDto.Description,
                Price = mealDto.Price,
                DayId = dayId,
            };
        }
      //  public static Meal ToMealDto(this CreateMealWithIdsDto mealIdDto, int dayId)
     //   {
      //      return new Meal
      //      {
                
        //        DayId = dayId,

            

     //       };
     //   }
    }
}