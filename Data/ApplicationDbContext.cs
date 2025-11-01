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

            optionsBuilder.UseSqlServer("Server=db29902.public.databaseasp.net; Database=db29902; User Id=db29902; Password=a#2EF5k@6!Pz; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
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
