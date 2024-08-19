namespace api.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        
        
        public int? DayId { get; set; }
        public Category? Category { get; set; }
        public Day? Day { get; set; }
        

        public List<ChoiseMealsSwap> ChoiseMeals { get; set; } = [];// не показываем но храним
    }
}