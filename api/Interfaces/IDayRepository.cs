using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Day;
using api.Models;

namespace api.Interfaces
{
    public interface IDayRepository
    {
        Task<List<Day>> GetAllAsync();

        Task<Day?>GetByIdAsync(int id); // FirstOfDefault CAN BE NULL
        Task<Day> CreateAsync (Day dayModel);   
        Task<Day?> UpdateAsync (int id, UpdateDayRequestDto dayDto);
        Task<Day?> DeleteAsync(int id);
        Task<bool> DayExists(int id);
    }
}