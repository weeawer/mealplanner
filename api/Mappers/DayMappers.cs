using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Day;
using api.Models;

namespace api.Mappers
{
    public static class DayMappers
    {
        public static DayDto ToDayDto(this Day dayModel )
        {
            return new DayDto
            {
                    Id = dayModel.Id,
                    
                    Meals = dayModel.Meals.Select(c => c.ToMealDto()).ToList()
            };
        }

        public static Day ToDayFromCreateDto(this CreateDayRequestDto dayDto)
        {
            return new Day
            {
                

            };
        }

    }
}