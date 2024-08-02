using api.Dtos.Meal;
using api.Models;
using api.Dtos.Category;

namespace api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,


            };
        }
    }
}
