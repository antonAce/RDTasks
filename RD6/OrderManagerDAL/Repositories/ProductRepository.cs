using System;
using System.Linq;
using System.Collections.Generic;

using OrderManagerDAL.Interfaces;
using OrderManagerDAL.Contexts;
using OrderManagerDAL.Models;

namespace OrderManagerDAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationContext _dbcontext;
        public ProductRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(Product entity) { _dbcontext.Products.Add(entity); }

        public void Delete(Product entity) { _dbcontext.Products.Remove(entity); }

        public void DeleteByKey(string key)
        {
            Product product = _dbcontext.Products.Find(key);
            if (product != null) Delete(product);
        }

        public IQueryable<Product> GetAll() {
            IQueryable<Product> products = _dbcontext.Products;

            foreach (var product in products)
                _dbcontext.Entry(product).Collection(p => p.OrderProducts).Load();

            return products;
        }

        public IQueryable<Product> GetByCondition(Func<Product, bool> predicate) {
            IQueryable<Product> products = _dbcontext.Products.Where(predicate).AsQueryable();

            foreach (var product in products)
                _dbcontext.Entry(product).Collection(p => p.OrderProducts).Load();

            return products;
        }

        public Product GetByKey(string key) {
            Product product = _dbcontext.Products.Find(key);

            if (product != null)
                _dbcontext.Entry(product).Collection(p => p.OrderProducts).Load();

            return product;
        }

        public void Update(Product entity) { _dbcontext.Products.Update(entity); }
    }
}
