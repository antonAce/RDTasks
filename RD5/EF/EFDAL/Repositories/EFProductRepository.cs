using System;
using System.Linq;

using EFDAL.Contexts;
using EFDAL.Interfaces;
using EFDAL.Models;

namespace EFDAL.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationContext _dbcontext;

        public EFProductRepository(ApplicationContext appContext) { _dbcontext = appContext; }

        public void Create(Product entity) { _dbcontext.Products.Add(entity); }

        public void Delete(Product entity) { _dbcontext.Products.Remove(entity); }

        public void DeleteByKey(string key)
        {
            Product product = _dbcontext.Products.Find(key);
            if (product != null) Delete(product);
        }

        public IQueryable<Product> GetAll() { return _dbcontext.Products; }

        public IQueryable<Product> GetByCondition(Func<Product, bool> predicate) { return _dbcontext.Products.Where(predicate).AsQueryable(); }

        public Product GetByKey(string key) { return _dbcontext.Products.Find(key); }

        public void Update(Product entity) { _dbcontext.Products.Update(entity); }
    }
}
