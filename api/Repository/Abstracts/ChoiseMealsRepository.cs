using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository.Abstracts
{
    public class ChoiseMealsRepository(ApplicationDbContext context) : IChoiseMealsRepository
    {
        public ApplicationDbContext _context = context;

        public async Task AddAsync(ChoiseMeal choiseMeal)
        {
            await _context.AddAsync(choiseMeal);
            await _context.SaveChangesAsync();
        }
    }
}
