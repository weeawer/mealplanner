using api.Models;

namespace api.Interfaces
{
    public interface IChoiseMealsSwapRepository
    {
        Task AddAsync(ChoiseMealsSwap choiseMeal);
    }
}
