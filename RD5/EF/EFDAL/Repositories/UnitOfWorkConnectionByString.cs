using System;

using EFDAL.Contexts;
using EFDAL.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace EFDAL.Repositories
{
    /// <summary>
    /// Class creates 'unit of work' for a database, by full connection string given in constructor param.
    /// Type of loading for all repositories: lazy loading (don't use for async loading!).
    /// </summary>
    public class UnitOfWorkConnectionByString : IUnitOfWork
    {
        private bool disposed = false;

        private ApplicationContext _dbContext;

        private EFCategoryRepository _categoryRepository;
        private EFProductRepository _productRepository;
        private EFVendorRepository _vendorRepository;

        public UnitOfWorkConnectionByString(string connectionString)
        {
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
