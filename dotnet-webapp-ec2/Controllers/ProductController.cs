using dotnet_webapp_ec2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;
using System.Linq;

namespace dotnet_webapp_ec2.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly NorthwindContext _db;

    public ProductController(ILogger<ProductController> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    // /Product or /Product/Index
    public IActionResult Index()
    {
        IEnumerable<Product> products = _db.Products
          .Include(p => p.Category)
          .Include(p => p.Supplier);

        if (!products.Any())
        {
            ViewBag.ErrorMessage = "No products found";
            return View();
        }

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        List<Category> categories = await _db.Categories.ToListAsync();
        ProductSearchModel model = new ProductSearchModel();
        model.Categories = categories;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Search(ProductSearchModel model)
    {
        List<Category> categories = await _db.Categories.ToListAsync();

        IEnumerable<Product> products = _db.Products            
          .Include(p => p.Category)
          .Include(p => p.Supplier)
          .Where(p => EF.Functions.Like(p.ProductName, $"%{model.ProductName!}%"));
        
        if (model.CategoryId != null)
        {
            products = products.Where(p => p.CategoryId == model.CategoryId);
        }

        if (model.Price != null)
        {
            if (model.PriceType == "&gt;")
                products = products.Where(p => p.UnitPrice > model.Price);
            if (model.PriceType == "&gt;=")
                products = products.Where(p => p.UnitPrice >= model.Price);
            if (model.PriceType == "&lt;")
                products = products.Where(p => p.UnitPrice < model.Price);
            if (model.PriceType == "&lt;=")
                products = products.Where(p => p.UnitPrice <= model.Price);
        }

        model.Categories = categories;
        model.Products = products.ToList();
        model.RequestMethod = Request.Method;

        return View(model);
    }

    public async Task<IActionResult> Categories()
    {
        List<Category> categories = await _db.Categories.ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> CategoryDetail(int? id)
    {
        if (!id.HasValue)
        {
            ViewBag.ErrorMessage = "No category selected.";
            return View();
        }

        Category? model = await _db.Categories.Include(p => p.Products).SingleOrDefaultAsync(p => p.CategoryId == id);

        if (model is null)
        {
            ViewBag.ErrorMessage = "Category not found.";
            return View();
        }

        return View(model); // Pass model to view and then return result.
    }
}
