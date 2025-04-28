using Microsoft.EntityFrameworkCore;

namespace DotBB.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DotBBContext(serviceProvider.GetRequiredService<DbContextOptions<DotBBContext>>()))
        {
            // Add categories
            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.AddRange(
                new Category
                {
                    Name = "DotBB",
                    Order = 0,
                },
                new Category
                {
                    Name = "Community",
                    Order = 1,
                }
            );

            context.SaveChanges();

            context.Subcategories.AddRange(
                new Subcategory
                {
                    Name = "Announcements",
                    Description = "Announcements about DotBB",
                    Order = 0,
                    Category = context.Categories.First(c => c.Name == "DotBB")
                },
                new Subcategory
                {
                    Name = "Guides",
                    Description = "Learn how to install and configure DotBB",
                    Order = 1,
                    Category = context.Categories.First(c => c.Name == "DotBB")
                },
                new Subcategory
                {
                    Name = "Help",
                    Description = "Get help installing and configuring DotBB",
                    Order = 1,
                    Category = context.Categories.First(c => c.Name == "Community")
                },
                new Subcategory
                {
                    Name = "Built with DotBB",
                    Description = "Let others know about your community",
                    Order = 1,
                    Category = context.Categories.First(c => c.Name == "Community")
                },
                new Subcategory
                {
                    Name = "Random",
                    Description = "For everything else!",
                    Order = 1,
                    Category = context.Categories.First(c => c.Name == "Community")
                }
            );

            context.SaveChanges();
        }
    }
}
