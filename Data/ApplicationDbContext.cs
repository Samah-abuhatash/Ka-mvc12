using KaShop1.Models;
using Microsoft.EntityFrameworkCore;


namespace KaShop1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Catgery> catgeries { get; set; }
        public DbSet<Proudct> Proudcts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=.;Database=Ka_12M;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Catgery>().HasData(
                new Catgery { Id = 1, Name = "Mobiles" },
            new Catgery { Id = 2, Name = "tablet" },
            new Catgery { Id = 3, Name = "Laptop" });

        }
    }
}
