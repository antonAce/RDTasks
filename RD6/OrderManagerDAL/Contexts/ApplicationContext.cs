using Microsoft.EntityFrameworkCore;

using OrderManagerDAL.Models;
using OrderManagerDAL.Configs;

namespace OrderManagerDAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new OrderProductConfig());


            modelBuilder.ApplyConfiguration(new OrderDefaultDataConfig());
            modelBuilder.ApplyConfiguration(new ProductDefaultDataConfig());
            modelBuilder.ApplyConfiguration(new OrderProductDefaultDataConfig());
        }
    }
}
