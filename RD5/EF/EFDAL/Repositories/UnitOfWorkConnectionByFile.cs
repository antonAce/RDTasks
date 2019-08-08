using System;
using System.IO;

using EFDAL.Contexts;
using EFDAL.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFDAL.Repositories
{
    /// <summary>
    /// Class creates 'unit of work' for a database, by name of JSON configuration file, located in project hierarcy, and connection name.
    /// Type of loading for all repositories: lazy loading (don't use for async loading!).
    /// </summary>
    public class UnitOfWorkConnectionByFile : IUnitOfWork
    {
        private bool disposed = false;

        private ApplicationContext _dbContext;

        private EFCategoryRepository _categoryRepository;
        private EFProductRepository _productRepository;
        private EFVendorRepository _vendorRepository;

        public UnitOfWorkConnectionByFile(string fileName, string connectionName)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(fileName);
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString(connectionName);
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;

            _dbContext = new ApplicationContext(options);
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new EFCategoryRepository(_dbContext);
                return _categoryRepository;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new EFProductRepository(_dbContext);
                return _productRepository;
            }
        }

        public IVendorRepository Vendors
        {
            get
            {
                if (_vendorRepository == null)
                    _vendorRepository = new EFVendorRepository(_dbContext);
                return _vendorRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) { _dbContext.Dispose(); }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges() { _dbContext.SaveChanges(); }
    }
}
