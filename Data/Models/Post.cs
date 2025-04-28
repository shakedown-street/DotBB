namespace DotBB.Data;

public class Post
{
  public int Id { get; set; }
  public User User { get; set; }
  public Thread Thread { get; set; }
  public string Content { get; set; }
}