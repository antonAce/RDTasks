using System;
using System.Linq;
using System.Collections.Generic;

using OrderManagerDAL.Interfaces;
using OrderManagerDAL.Models;

using OrderManagerBLL.DTO;
using OrderManagerBLL.Interfaces;

namespace OrderManagerBLL.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _dbcontext;

        public ProductService(IUnitOfWork unitOfWork) { _dbcontext = unitOfWork; }

        public void AddProduct(ProductDTO product)
        {
            _dbcontext.Products.Create(new Product {
                GTIN = product.GTIN,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            });

            _dbcontext.SaveChanges();
        }

        public IEnumerable<ProductDTO> ListProducts()
        {
            IEnumerable<Product> products = _dbcontext.Products.GetAll();

            foreach (Product product in products)
                yield return new ProductDTO {
                    GTIN = product.GTIN,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                };
        }

        public ProductDTO GetProductByGTIN(string GTIN)
        {
            Product product = _dbcontext.Products.GetByKey(GTIN);

            if (product != null)
                return new ProductDTO {
                    GTIN = product.GTIN,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price };
            else
                return null;
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}
