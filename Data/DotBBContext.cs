using Microsoft.EntityFrameworkCore;

namespace DotBB.Data;

public class DotBBContext : DbContext
{
    public DotBBContext(DbContextOptions<DotBBContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<Thread> Threads { get; set; }
    public DbSet<Post> Posts { get; set; }
}
