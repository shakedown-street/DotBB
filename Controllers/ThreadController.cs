using System.Security.Claims;
using DotBB.Data;
using DotBB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotBB.Controllers;

public class ThreadController : Controller
{
    private readonly DotBBContext _context;

    public ThreadController(DotBBContext _context)
    {
        this._context = _context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create(int? subcategoryId)
    {
        if (subcategoryId == null)
        {
            return BadRequest("Subcategory ID is required.");
        }

        var subcategory = await _context.Subcategories.FindAsync(subcategoryId);
        if (subcategory == null)
        {
            return NotFound();
        }

        var model = new ThreadCreateViewModel
        {
            Subcategory = subcategory
        };

        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(int subcategoryId, string title, string content)
    {
        var subcategory = await _context.Subcategories.FindAsync(subcategoryId);
        if (subcategory == null)
        {
            return NotFound("Subcategory not found.");
        }

        var user = await _context.Users.FindAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (ModelState.IsValid)
        {


            var thread = new Data.Thread
            {
                Title = title,
                Content = content,
                Subcategory = subcategory,
                User = user,
            };
            _context.Threads.Add(thread);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Subcategory", new { id = subcategory.Id });
        }

        var model = new ThreadCreateViewModel
        {
            Subcategory = subcategory
        };

        return View(model);
    }
}