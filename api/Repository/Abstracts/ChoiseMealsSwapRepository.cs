using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository.Abstracts
{
    public class ChoiseMealsSwapRepository(ApplicationDbContext context) : IChoiseMealsSwapRepository
    {
        public ApplicationDbContext _context = context;

        

        public async Task AddAsync(ChoiseMealsSwap choiseMeal)
        {
            await _context.AddAsync(choiseMeal);
            await _context.SaveChangesAsync();
        }
    }
}
