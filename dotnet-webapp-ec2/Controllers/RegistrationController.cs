using Microsoft.AspNetCore.Mvc; // to use IActionResult, etc
using Northwind.EntityModels;  // to use NorthwindContext
using Microsoft.EntityFrameworkCore.ChangeTracking; // to use EntityEntry, etc
using Microsoft.EntityFrameworkCore; // DbUpdateException
using static PasswordHash.PasswordHash;

namespace dotnet_webapp_ec2.Controllers;

public class RegistrationController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly NorthwindContext _db;

    public RegistrationController(ILogger<ProductController> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(User user)
    {
        if (ModelState.IsValid)
        {
            try
            {
                User? uFromDb = _db.Users.FirstOrDefault(c => c.Username == user.Username);

                if (uFromDb is not null)
                {
                    ViewBag.ErrorMessage = "Username already exists.  Please try again.";
                    return View(user);
                }

                user.EncryptedPassword = CreateHash(user.Password);
                EntityEntry<User>? added = await _db.Users.AddAsync(user);

                int affected = await _db.SaveChangesAsync();
                if (affected == 1)
                {
                    ViewBag.SuccessMessage = "true";
                    return View(added.Entity);
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.Log(LogLevel.Error, ex, $"An error occurred: {ex.Message}");
                ViewBag.ErrorMessage = "A database error occurred.  Please try again later.";
            }
        }

        return View(user);
    }
}
