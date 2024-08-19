using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;



namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LunchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LunchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserMealsForToday(string id)
        {
            var today = DateTime.Today;

            // Получаем пользователя с его выбранными обедами на сегодняшний день
            var userMeals = await _context.Users
                .Include(u => u.ChoiseMeals)
                    .ThenInclude(cm => cm.Meal)
                .Include(u => u.ChoiseMeals)
                    .ThenInclude(cm => cm.Day)
                .Where(u => u.Id == id && u.ChoiseMeals.Any(cm => cm.Day.Date == today))
                .SelectMany(u => u.ChoiseMeals.Where(cm => cm.Day.Date == today))
                .Select(cm => cm.Meal)
                .ToListAsync();

            if (userMeals == null || !userMeals.Any())
            {
                return NotFound("Блюда на текущий день не найдены.");
            }

            return Ok(userMeals);
        }
    }
}

