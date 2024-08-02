using api.Data;
using api.Dtos.Day;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/day")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDayRepository _dayRepo;
        public DayController(ApplicationDbContext context, IDayRepository dayRepo)
        {
            _dayRepo = dayRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var days = await _dayRepo.GetAllAsync();//await _context.Days.ToListAsync();
            var DayDto = days.Select(s => s.ToDayDto()); // извлечь из БД то что было создано в ней
            return Ok(days);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var day = await _dayRepo.GetByIdAsync(id); //_context.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }
            return Ok(day.ToDayDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDayRequestDto dayDto)   //Frombody - передает в Json - фактической форме
        {
            var dayModel = dayDto.ToDayFromCreateDto();
            await _dayRepo.CreateAsync(dayModel);
            return CreatedAtAction(nameof(GetById), new { id = dayModel.Id }, dayModel.ToDayDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDayRequestDto updateDto)
        {
            var dayModel = await _dayRepo.UpdateAsync(id, updateDto);     //await _context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (dayModel == null)
            {
                return NotFound();
            }
            //dayModel.DayDate = updateDto.DayDate;
            // await _context.SaveChangesAsync();
            return Ok(dayModel.ToDayDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var dayModel = await _dayRepo.DeleteAsync(id);   //_context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (dayModel == null)
            {
                return NotFound();
            }
            //_context.Days.Remove(dayModel);
            //await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}