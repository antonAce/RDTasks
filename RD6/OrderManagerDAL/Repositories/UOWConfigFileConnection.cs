using System;
using System.IO;

using OrderManagerDAL.Contexts;
using OrderManagerDAL.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OrderManagerDAL.Repositories
{
    /// <summary>
    /// Class creates 'unit of work' for a database, by name of JSON configuration file, located in project hierarcy, and connection name.
    /// Type of loading for all repositories: explicit loading.
    /// </summary>
    public class UOWConfigFileConnection : IUnitOfWork
    {
        private bool _disposed = false;
        private ApplicationContext _dbcontext;

        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;

        public UOWConfigFileConnection(string filename, string connectionname)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(filename);
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString(connectionname);
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;

            _dbcontext = new ApplicationContext(options);
        }

        public IProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_dbcontext);
                return _productRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_dbcontext);
                return _orderRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) { _dbcontext.Dispose(); }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges() { _dbcontext.SaveChanges(); }
    }
}
