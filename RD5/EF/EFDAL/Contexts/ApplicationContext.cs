using Microsoft.EntityFrameworkCore;

using EFDAL.Models;
using EFDAL.Configs;

namespace EFDAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { Database.EnsureCreated(); }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=DESKTOP-3CU7O3P\SQLEXPRESS;Database=EFTask5;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CategoryDefaultDataConfig());

            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new ProductDefaultDataConfig());

            modelBuilder.ApplyConfiguration(new VendorConfig());
            modelBuilder.ApplyConfiguration(new VendorDefaultDataConfig());
        }
    }
}
