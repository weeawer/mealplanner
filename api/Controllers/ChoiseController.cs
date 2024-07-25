using api.Dtos.Choise;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/choise")]
    [ApiController]
    public class ChoiseController(IMealRepository mealRepository, IChoiseMealsRepository choiseMealsRepository, IDayRepository dayRepository, IAppUserRepository appUserRepository) : ControllerBase
    {
        private readonly IChoiseMealsRepository _choiseMealsRepository = choiseMealsRepository;
        private readonly IDayRepository _dayRepository = dayRepository;
        private readonly IAppUserRepository _appUserRepository = appUserRepository;
        private readonly IMealRepository _mealRepository = mealRepository;

        [HttpPost("add-meals")]
        public async Task<IActionResult> AddMeals(List<DayMealsDto> dayMealsDtos)
        {
            foreach (var dayMealsDto in dayMealsDtos)
            {
                var user = await _appUserRepository.GetByIdAsync(dayMealsDto.AppUserId);
                var day = await _dayRepository.GetByIdAsync(dayMealsDto.DayId);

                if (user is null)
                    return NotFound($"Choice not found for UserId {dayMealsDto.AppUserId}");
                if (day is null)
                    return NotFound($"Choice not found for DayId {dayMealsDto.DayId}");
                foreach (var item in dayMealsDto.MealsId)
                {
                    var meal = await _mealRepository.GetByIdAsync(item);

                    if (meal is not null && !day.ChoiseMeals.Any(cm => cm.MealId == item && cm.DayId == day.Id && cm.AppUserId == user.Id))
                    {
                        var choiceMeal = new ChoiseMeal()
                        {
                            AppUserId = user.Id,
                            DayId = day.Id,
                            MealId = item,
                        };
                        await _choiseMealsRepository.AddAsync(choiceMeal);
                    }
                }
            }

            return Ok("Meals added successfully");
        }


    }
}


