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
using MyOnlineCraftWeb.Static_details;

namespace MyOnlineCraftWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OnlineCraftStoreDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(OnlineCraftStoreDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var onlineCraftStoreDbContext = _context.Products.Include(p => p.Category);
            return View(await onlineCraftStoreDbContext.ToListAsync());
        }

        // GET: Products/Create
        [Authorize(Roles = StaticDetails.roleAdmin)]

        public IActionResult Create()
        {
            ViewData["productCategoryId"] = new SelectList(_context.Categories, "catId", "categoryName");
            return View();
        }

        // POST: Products/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            string getimageURL = UploadFile(product.imageURL);
            
            if (ModelState.IsValid)
            {
                product.imageURL = getimageURL;
                product.discountPercent = CalculateDiscount(product.DiscountPrice, product.ActualPrice);


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productCategoryId"] = new SelectList(_context.Categories, "catId", "categoryName", product.productCategoryId);
            return View(product);
        }

        private string UploadFile(string productimageURL)
        {
            String imageDrive = productimageURL;
            string imageString = "https://drive.google.com/uc?export=view&id=";
            if (imageDrive != null)
            {
                String[] imageSplit = imageDrive.Split("/");
                var id= imageSplit[5];
                imageString += id;
            }
            return imageString;
        }

        // GET: Products/Edit/5
        [Authorize(Roles = StaticDetails.roleAdmin)]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["productCategoryId"] = new SelectList(_context.Categories, "catId", "categoryName", product.productCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.productId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["productCategoryId"] = new SelectList(_context.Categories, "catId", "categoryName", product.productCategoryId);
            return View(product);
        }

        private static int CalculateDiscount(double discountPrice, double actualPrice)
        {
            return (int)Convert.ToSingle((actualPrice - discountPrice) / actualPrice * 100);  
        }

        // GET: Products/Delete/5
        [Authorize(Roles = StaticDetails.roleAdmin)]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.productId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OnlineCraftStoreDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.productId == id)).GetValueOrDefault();
        }
    }
}
