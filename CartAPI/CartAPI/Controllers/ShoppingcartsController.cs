using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartAPI.Models;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingcartsController : ControllerBase
    {
        private readonly OnlineCraftStoreContext _context;

        public ShoppingcartsController(OnlineCraftStoreContext context)
        {
            _context = context;
        }

        // GET: api/Shoppingcarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoppingcart>>> GetShoppingcarts()
        {
            return await _context.Shoppingcarts.ToListAsync();
        }

        // GET: api/Shoppingcarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoppingcart>> GetShoppingcart(int id)
        {
            var shoppingcart = await _context.Shoppingcarts.FindAsync(id);

            if (shoppingcart == null)
            {
                return NotFound();
            }

            return shoppingcart;
        }

        // PUT: api/Shoppingcarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingcart(int id, Shoppingcart shoppingcart)
        {
            if (id != shoppingcart.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingcart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingcartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shoppingcarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shoppingcart>> PostShoppingcart(Shoppingcart shoppingcart)
        {
            _context.Shoppingcarts.Add(shoppingcart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingcart", new { id = shoppingcart.Id }, shoppingcart);
        }

        // DELETE: api/Shoppingcarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingcart(int id)
        {
            var shoppingcart = await _context.Shoppingcarts.FindAsync(id);
            if (shoppingcart == null)
            {
                return NotFound();
            }

            _context.Shoppingcarts.Remove(shoppingcart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingcartExists(int id)
        {
            return _context.Shoppingcarts.Any(e => e.Id == id);
        }
    }
}
