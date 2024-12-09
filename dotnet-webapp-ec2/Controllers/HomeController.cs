using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_webapp_ec2.Models;
using Northwind.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapp_ec2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NorthwindContext _db;

    public HomeController(ILogger<HomeController> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    // Handles / or /Index or /Home/Index
    public async Task<IActionResult> Index()
    {
        List<Category> categories = await _db.Categories.ToListAsync();
        return View(categories);
    }

    // /Home/Privacy
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
