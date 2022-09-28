using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CartAPI.Models
{
    public partial class OnlineCraftStoreContext : DbContext
    {
        public OnlineCraftStoreContext()
        {
        }

        public OnlineCraftStoreContext(DbContextOptions<OnlineCraftStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Shoppingcart> Shoppingcarts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=OnlineCraftStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.HasIndex(e => e.AppUserId, "IX_Shoppingcarts_AppUserId");

                entity.HasIndex(e => e.ProductId, "IX_Shoppingcarts_ProductID");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
