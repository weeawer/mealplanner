using api.Data;
using api.Dtos.Meal;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/meal")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMealRepository _mealRepo;
        private readonly IDayRepository _dayRepo;
        public MealController(ApplicationDbContext context, IMealRepository mealRepo, IDayRepository dayRepo )
        {
            _mealRepo = mealRepo;
            _context = context;
            _dayRepo = dayRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meals = await _mealRepo.GetAllAsync();//await _context.Days.ToListAsync();
            var MealDto = meals.Select(s => s.ToMealDto()); // извлечь из БД то что было создано в ней

                 return Ok(meals);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var meal = await _mealRepo.GetByIdAsync(id); //_context.Days.FindAsync(id);

            if (meal == null)
            {
                return NotFound();  

            }

            return Ok(meal.ToMealDto());
        }

        [HttpPost("{dayId}")]
        public async Task<IActionResult> Create([FromRoute] int dayId, CreateMealRequestDto mealDto)   //Frombody - передает в Json - фактической форме
        {
            if(!await _dayRepo.DayExists(dayId))
            {
            return BadRequest("Day is not exist");
            }
            var mealModel = mealDto.ToMealFromCreate(dayId);
            await _mealRepo.CreateAsync(mealModel);
            return CreatedAtAction(nameof(GetById), new {id = mealModel}, mealModel.ToMealDto());
        }




        [HttpPost("{dayId}/add-meals")]
        public async Task<IActionResult> AddMealsToDay([FromRoute] int dayId, [FromBody] CreateMealWithIdsDto mealIdsDto)
        {
            if (!await _dayRepo.DayExists(dayId))
            {
            return BadRequest("Day does not exist");
            }

            foreach (var mealId in mealIdsDto.MealsIds)
            {
        

        // Создание записи связывания mealId и dayId
            var mealModel = new Meal
            {
            DayId = dayId,
            Id = mealId
            };
        }

        return Ok("Meals have been successfully added to the day");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] UpdateMealRequestDto updateDto)
        {
        var mealModel = await _mealRepo.UpdateAsync(id, updateDto);     //await _context.Days.FirstOrDefaultAsync(x => x.Id == id);
        if(mealModel == null)
        {
            return NotFound();
        }
        //dayModel.DayDate = updateDto.DayDate;
       // await _context.SaveChangesAsync();
        return Ok(mealModel.ToMealDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
        var mealModel = await _mealRepo.DeleteAsync(id);   //_context.Days.FirstOrDefaultAsync(x => x.Id == id);

        if (mealModel == null)
        {
            return NotFound();
        }

        //_context.Days.Remove(dayModel);

        //await _context.SaveChangesAsync();

        return NoContent();
        }


    }
}