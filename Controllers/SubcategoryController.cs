using Microsoft.AspNetCore.Mvc;
using DotBB.Data;
using DotBB.Models;
using Microsoft.EntityFrameworkCore;

namespace DotBB.Controllers;

public class SubcategoryController : Controller
{
    private readonly DotBBContext _context;

    public SubcategoryController(DotBBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subcategory = await _context.Subcategories
            .Include(s => s.Category)
            .Include(s => s.Threads)
            .ThenInclude(t => t.User)
            .ThenInclude(t => t.Posts)
            .FirstOrDefaultAsync(s => s.Id == id);

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
