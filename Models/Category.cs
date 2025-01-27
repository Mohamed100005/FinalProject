namespace Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CityCategory> CityCategories { get; set; } = new List<CityCategory>();
    }
}
