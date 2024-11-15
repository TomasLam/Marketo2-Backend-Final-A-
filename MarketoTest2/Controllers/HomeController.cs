using Microsoft.AspNetCore.Mvc;
using MarketoTest2.Models;
using MarketoTest2.Data;
using System.Diagnostics;

namespace MarketoTest2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
             _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            

            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult RegSite()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
