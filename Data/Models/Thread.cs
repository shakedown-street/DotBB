namespace DotBB.Data;

public class Thread
{
    public int Id { get; set; }
    public User User { get; set; }
    public Subcategory Subcategory { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<Post> Posts { get; set; }
}