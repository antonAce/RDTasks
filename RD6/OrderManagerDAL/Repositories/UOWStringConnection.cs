using System;

using OrderManagerDAL.Contexts;
using OrderManagerDAL.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace OrderManagerDAL.Repositories
{
    /// <summary>
    /// Class creates 'unit of work' for a database, by full connection string given in constructor param.
    /// Type of loading for all repositories: explicit loading.
    /// </summary>
    public class UOWStringConnection : IUnitOfWork
    {
        private bool _disposed = false;
        private ApplicationContext _dbcontext;

        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;

        public UOWStringConnection(string connectionString)
        {
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
