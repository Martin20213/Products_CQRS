using Microsoft.EntityFrameworkCore;
using Products_CQRS.Domain.Models;

namespace Products_CQRS.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //egy kategorianak tobb termeke lehet
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }


    }
}
