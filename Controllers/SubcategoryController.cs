using Microsoft.AspNetCore.Mvc;
using DotBB.Data;
using DotBB.Models;
using Microsoft.EntityFrameworkCore;

namespace DotBB.Controllers;

public class SubcategoryController : Controller
{
    private readonly DotBBDbContext _context;

    public SubcategoryController(DotBBDbContext context)
    {
        _context = context;
    }

    public IActionResult Details(int id)
    {
        var subcategory = _context.Subcategories
            .Include(s => s.Category) // Include related Category
            .FirstOrDefault(s => s.Id == id);

        if (subcategory == null)
        {
            return NotFound();
        }

        var viewModel = new SubcategoryViewModel
        {
            Subcategory = subcategory
        };

        return View(viewModel);
    }
}
