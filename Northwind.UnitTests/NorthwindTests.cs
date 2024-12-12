using Microsoft.AspNetCore.Mvc; // to use ViewResult, etc.
using Microsoft.AspNetCore.Http;
using dotnet_webapp_ec2.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Northwind.EntityModels;
using static PasswordHash.PasswordHash;
using Microsoft.EntityFrameworkCore;
using System.IO;
using dotnet_webapp_ec2.Models;

namespace Northwind.UnitTests;

public class NorthwindTests
{
    ILogger<HomeController> homeLogger;
    ILogger<CustomersController> custLogger;
    ILogger<EmployeesController> empLogger;
    ILogger<ProductController> productLogger;
    private NorthwindContext db;

    public NorthwindTests()
    {
        homeLogger = new Mock<ILogger<HomeController>>().Object;
        custLogger = new Mock<ILogger<CustomersController>>().Object;
        empLogger = new Mock<ILogger<EmployeesController>>().Object;
        productLogger = new Mock<ILogger<ProductController>>().Object;
        db = new();
    }

    [Fact]
    public async Task Index_ReturnsAViewResult_With_8_Categories()
    {
        // Arrange        
        HomeController controller = new HomeController(homeLogger, db);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Category>>(viewResult.ViewData.Model);
        Assert.Equal(8, model.Count);
    }

    [Fact]
    public async Task Customers_Index_ReturnsAViewResult_With_91_Customers()
    {
        // Arrange        
        CustomersController controller = new CustomersController(custLogger, db);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Customer>>(viewResult.ViewData.Model);
        Assert.Equal(91, model.Count);
    }

    [Fact]
    public async Task Employees_Index_ReturnsAViewResult_With_9_Employees()
    {
        // Arrange        
        EmployeesController controller = new EmployeesController(empLogger, db);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Employee>>(viewResult.ViewData.Model);
        Assert.Equal(9, model.Count);
    }

    [Fact]
    public async Task Product_Index_ReturnsAViewResult_With_77_Products()
    {
        // Arrange        
        ProductController controller = new ProductController(productLogger, db);

        // Act
        IActionResult result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);
        Assert.Equal(77, model.Count);
    }

    [Fact]
    public async Task Product_Search_Index_ReturnsAViewResult_With_0_Products_8_Categories()
    {
        // Arrange        
        ProductController controller = new ProductController(productLogger, db);

        // Act
        IActionResult result = await controller.Search();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ProductSearchModel>(viewResult.ViewData.Model);
        Assert.Equal(8, model.Categories?.Count);
        Assert.Null(model.Products);
    }
    
    [Fact]
    public async Task Product_Search_Beverages_ReturnsAViewResult_With_12_Products()
    {
        // Arrange        
        ProductController controller = new ProductController(productLogger, db);
        ProductSearchModel model = new();
        model.CategoryId = 1;

        // Act
        IActionResult result = await controller.Search(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        model = Assert.IsAssignableFrom<ProductSearchModel>(viewResult.ViewData.Model);
        Assert.Equal(12, model.Products?.Count);
    }

    [Fact]
    public async Task Product_Search_Name_C_ReturnsAViewResult_With_37_Products()
    {
        // Arrange        
        ProductController controller = new ProductController(productLogger, db);
        ProductSearchModel model = new();
        model.ProductName = "c";

        // Act
        IActionResult result = await controller.Search(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        model = Assert.IsAssignableFrom<ProductSearchModel>(viewResult.ViewData.Model);
        Assert.Equal(37, model.Products?.Count);
    }

    [Fact]
    public async Task Product_Search_Price_gteq_35_ReturnsAViewResult_With_17_Products()
    {
        // Arrange        
        ProductController controller = new ProductController(productLogger, db);
        ProductSearchModel model = new();
        model.PriceType = "&gt;=";
        model.Price = 35;

        // Act
        IActionResult result = await controller.Search(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        model = Assert.IsAssignableFrom<ProductSearchModel>(viewResult.ViewData.Model);
        Assert.Equal(17, model.Products?.Count);
    }

    [Fact]
    public async Task Product_Search_Price_lteq_35_ReturnsAViewResult_With_60_Products()
    {
        // Arrange        
        ProductController controller = new ProductController(productLogger, db);
        ProductSearchModel model = new();
        model.PriceType = "&lt;=";
        model.Price = 35;

        // Act
        IActionResult result = await controller.Search(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        model = Assert.IsAssignableFrom<ProductSearchModel>(viewResult.ViewData.Model);
        Assert.Equal(17, model.Products?.Count);
    }

    [Fact]
    public void Password_Encrypts_and_Compares_25()
    {
        string password = "password10thisissten12345";
        string passwdEncrypted = CreateHash(password);

        Assert.True(ValidatePassword(password, passwdEncrypted));
    }

    [Fact]
    public void Password_Encrypts_and_Compares_8()
    {
        string password = "P@ssw/8c";
        string passwdEncrypted = CreateHash(password);

        Assert.True(ValidatePassword(password, passwdEncrypted));
    }
}