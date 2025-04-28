namespace DotBB.Data;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public Category Category { get; set; }
    public ICollection<Thread> Threads { get; set; }
}

// INSERT INTO "Subcategories" ("Name", "Slug", "Description", "Order", "CategoryId") VALUES ('Announcements', 'announcements', 'Annoucements about DotBB', 1, 2);
