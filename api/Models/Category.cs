namespace api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
