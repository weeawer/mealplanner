namespace api.Dtos.Choise
{
    public class DayMealsDto
    {
        public string AppUserId { get; set; }
        public int DayId { get; set; }
        public List<int> MealsId { get; set; }
    }
}
