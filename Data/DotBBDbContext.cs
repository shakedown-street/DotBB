namespace DotBB.Data;
using Microsoft.EntityFrameworkCore;

public class DotBBDbContext : DbContext
{
  public DotBBDbContext(DbContextOptions<DotBBDbContext> options) : base(options) { }

  // public DbSet<User> Users { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Subcategory> Subcategories { get; set; }
  // public DbSet<DotBBThread> Threads { get; set; }
  // public DbSet<DotBBPost> Posts { get; set; }
}
