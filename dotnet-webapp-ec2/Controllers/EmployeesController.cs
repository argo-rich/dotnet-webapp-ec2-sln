using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;

namespace dotnet_webapp_ec2.Controllers;

[Authorize]
public class EmployeesController : Controller
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly NorthwindContext _db;

    public EmployeesController(ILogger<EmployeesController> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        List<Employee> employees = await _db.Employees.OrderBy(e => e.FirstName).ToListAsync();

        if (!employees.Any())
        {
            ViewBag.ErrorMessage = "No employees found";
            return View();
        }

        return View(employees);
    }
}
