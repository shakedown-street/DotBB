namespace DotBB.Data;

public class Thread
{
    public int Id { get; set; }
    public User User { get; set; }
    public Subcategory Subcategory { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<Post> Posts { get; set; }

    public int PostCount => Posts?.Count ?? 0;
}

// INSERT INTO "Threads" ("UserId", "SubcategoryId", "Title", "Content") VALUES (1, 1, 'Welcome to DotBB', 'Welcome to DotBB! This is a test thread.');