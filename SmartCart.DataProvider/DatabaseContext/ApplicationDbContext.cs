using Microsoft.EntityFrameworkCore;
using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartMember> CartMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Cart>().ToTable("Carts");
            modelBuilder.Entity<CartMember>().ToTable("CartMembers");

            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Cart>().HasKey(c => c.CartID);
            modelBuilder.Entity<CartMember>().HasKey(cm => cm.CartMemberID);
        }
    }
}
