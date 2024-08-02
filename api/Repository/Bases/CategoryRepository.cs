using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

namespace api.Repository.Bases
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }



    }
}
