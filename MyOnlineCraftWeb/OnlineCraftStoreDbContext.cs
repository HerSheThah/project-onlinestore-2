using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyOnlineCraftWeb.Models;
using MyOnlineCraftWeb.Models.ViewModel;

namespace MyOnlineCraftWeb
{
    public class OnlineCraftStoreDbContext:IdentityDbContext
    {
        public OnlineCraftStoreDbContext(DbContextOptions<OnlineCraftStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Shoppingcart> Shoppingcarts { get; set; }

        
    }
}
