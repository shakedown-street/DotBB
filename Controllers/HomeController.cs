using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotBB.Data;
using DotBB.Models;

namespace DotBB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DotBBDbContext _context;

    public HomeController(ILogger<HomeController> logger, DotBBDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        var viewModel = new IndexViewModel
        {
            Categories = categories
        };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
