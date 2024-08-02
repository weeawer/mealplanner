using api.Data;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using System.Globalization;

namespace api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IDayRepository _dayRepo;
        public CategoryController(ApplicationDbContext context, ICategoryRepository categoryRepo, IDayRepository dayRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _dayRepo = dayRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepo.GetAllAsync();
            var categoryDtos = categories.Select(s => s.ToCategoryDto());
            var days = await _dayRepo.GetAllAsync();
            var dayDtos = days.Select(s => s.ToDayDto());
            DateTime today = DateTime.Today;
            int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextMonday = today.AddDays(daysUntilNextMonday);
            string nextMondayFormatted = nextMonday.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            var result = new
            {
                Start_date = nextMondayFormatted,
                Categories = categoryDtos,
                Ration = dayDtos
            };
            return Ok(result);
        }
    }
}