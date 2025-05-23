using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotBB.Data;
using DotBB.Models;

namespace DotBB.Controllers;

public class HomeController : Controller
{
    private readonly DotBBContext _context;

    public HomeController(DotBBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories
            .Include(c => c.Subcategories)
            .ThenInclude(s => s.Threads)
            .ToList();
        var viewModel = new IndexViewModel
        {
            Categories = categories
        };
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
