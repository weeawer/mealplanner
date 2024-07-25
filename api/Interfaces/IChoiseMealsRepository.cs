using api.Models;

namespace api.Interfaces
{
    public interface IChoiseMealsRepository
    {
        Task AddAsync(ChoiseMeal choiseMeal);
    }
}
