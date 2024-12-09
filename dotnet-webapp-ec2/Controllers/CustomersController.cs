using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;

namespace dotnet_webapp_ec2.Controllers;

[Authorize]
public class CustomersController : Controller
{
    private readonly ILogger<CustomersController> _logger;
    private readonly NorthwindContext _db;

    public CustomersController(ILogger<CustomersController> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        List<Customer> customers = await _db.Customers.ToListAsync();

        if (!customers.Any())
        {
            ViewBag.ErrorMessage = "No customers found";
            return View();
        }

        return View(customers);
    }
}
