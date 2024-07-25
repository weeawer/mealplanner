namespace api.Models
{
    public class ChoiseMeal
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }      
    }
}
