using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using DotBB.Data;

namespace DotBB.Controllers;

public class AccountController : Controller
{
    private readonly DotBBContext _context;

    public AccountController(DotBBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(string username, string password1, string password2)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(password2))
        {
            ModelState.AddModelError("", "All fields are required.");
            return View();
        }

        if (password1 != password2)
        {
            ModelState.AddModelError("", "Passwords do not match.");
            return View();
        }

        if (password1.Length < 6)
        {
            ModelState.AddModelError("", "Password must be at least 6 characters long.");
            return View();
        }

        if (!username.All(char.IsLetterOrDigit))
        {
            ModelState.AddModelError("", "Username can only contain letters and digits.");
            return View();
        }

        if (username.Length < 3 || username.Length > 20)
        {
            ModelState.AddModelError("", "Username must be between 3 and 20 characters long.");
            return View();
        }
        if (_context.Users.Any(u => u.Username == username))
        {
            ModelState.AddModelError("", "Username already exists.");
            return View();
        }

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = username,
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password1)),
            PasswordSalt = hmac.Key
        };
        _context.Users.Add(user);
        _context.SaveChanges();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);

        if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }
}