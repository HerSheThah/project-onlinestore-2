using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOnlineCraftWeb;
using MyOnlineCraftWeb.Models;
using MyOnlineCraftWeb.Models.ViewModel;

namespace MyOnlineCraftWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly OnlineCraftStoreDbContext _context;
        public ShoppingCartVM shoppingcartVM { get; set; }

        public CartController(OnlineCraftStoreDbContext context)
        {
            _context = context;
        }

        // GET: Shoppingcarts
        public async Task<IActionResult> Index()
        {

            var claimIndentity = (ClaimsIdentity)User.Identity;
            var claims = claimIndentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingcartVM = new ShoppingCartVM
            {
                shoppingcartList=_context.Shoppingcarts.Include(x=>x.Product).Where(x=>x.AppUserId==claims.Value).ToList(),
            };
            foreach(var item in shoppingcartVM.shoppingcartList)
            {
                shoppingcartVM.cartTotal += (item.Product.DiscountPrice * item.count);
            }
        
            return View(shoppingcartVM);
        }

        public IActionResult Plus(int cartId)
        {
            var cartItem = _context.Shoppingcarts.FirstOrDefault(u => u.Id == cartId);
            if(cartItem.count <= 999)
            {
                cartItem.count += 1;
                _context.Update(cartItem);
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartItem = _context.Shoppingcarts.FirstOrDefault(u => u.Id == cartId);
            
            cartItem.count -= 1;
            if(cartItem.count <= 0)
            {
                
                return RedirectToAction("Delete", new { cartId = cartId });
            }
            else 
            {
                _context.Update(cartItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
        }


        // GET: Shoppingcarts/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["ProductID"] = new SelectList(_context.Products, "productId", "productName");
            return View();
        }

        // POST: Shoppingcarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductID,count,AppUserId")] Shoppingcart shoppingcart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingcart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", shoppingcart.AppUserId);
            ViewData["ProductID"] = new SelectList(_context.Products, "productId", "productName", shoppingcart.ProductID);
            return View(shoppingcart);
        }

        // GET: Shoppingcarts/Delete/5
        public IActionResult Delete(int cartId)
        {
            if (_context.Shoppingcarts == null)
            {
                return Problem("Entity set 'OnlineCraftStoreDbContext.Products'  is null.");
            }
            var cart =  _context.Shoppingcarts.Find(cartId);
            if (cart != null)
            {
                _context.Shoppingcarts.Remove(cart);
            }

             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingcartExists(int id)
        {
          return _context.Shoppingcarts.Any(e => e.Id == id);
        }
    }
}
