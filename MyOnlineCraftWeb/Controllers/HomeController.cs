using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOnlineCraftWeb.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MyOnlineCraftWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OnlineCraftStoreDbContext _context;

        public HomeController(ILogger<HomeController> logger, OnlineCraftStoreDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Product> products = _context.Products.Include(x => x.Category).ToList();
            return View(products);
        }

        // GET: Products/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int productid)
        {
            if (productid == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.productId == productid);

            if (product == null)
            {
                return NotFound();
            }
            var cartObj = new Shoppingcart
            {
                Product = product,
                ProductID = productid,
                count = 1
            };

            return View(cartObj);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(Shoppingcart shoppingcart)
        {
            var claimIndentity = (ClaimsIdentity)User.Identity;
            var claims = claimIndentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingcart.AppUserId = claims.Value;
            var cartFromDB = _context.Shoppingcarts.FirstOrDefaultAsync
                (u => u.AppUserId == claims.Value && u.ProductID == shoppingcart.ProductID).Result;
            if (cartFromDB == null)
            {
                _context.Shoppingcarts.Add(shoppingcart);

            }
            else
            {
                cartFromDB.count += shoppingcart.count;
                _context.Update(cartFromDB);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

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
}