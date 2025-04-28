namespace DotBB.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public ICollection<Subcategory> Subcategories { get; set; }
}
