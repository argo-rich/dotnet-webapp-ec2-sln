using dotnet_webapp_ec2.Models;
using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels;
using System.Diagnostics.Eventing.Reader;
using static PasswordHash.PasswordHash;

namespace dotnet_webapp_ec2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _db;

        public LoginController(ILogger<HomeController> logger, NorthwindContext db)
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
        public IActionResult Index(LoginModel loginModel)
        {
            User? user = _db.Users
                .Where(u => u.Username == loginModel.Username)
                .FirstOrDefault();

            if (user != null && ValidatePassword(loginModel.Password, user.EncryptedPassword))
            {
                return Redirect("/");
            }

            else
            {
                ViewBag.ErrorMessage = "Username or Password is invalid.";
                return View(loginModel);                
            }
        }
    }
}
