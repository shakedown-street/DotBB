namespace DotBB.Models
{
  public class Subcategory
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public Category Category { get; set; }
  }
}