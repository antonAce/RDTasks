using System;
using System.Linq;
using System.Collections.Generic;

using EFDAL.Interfaces;
using EFDAL.Models;

using EFBLL.DTO;
using EFBLL.Interfaces;

using AutoMapper;

namespace EFBLL.Services
{
    public class DefaultProductService : IProductService
    {
        private IUnitOfWork _dbcontext { get; set; }
        private IMapper _productMapper;

        public DefaultProductService(IUnitOfWork uow)
        {
            _dbcontext = uow;
            _productMapper = new MapperConfiguration(config => config.CreateMap<Product, ProductDTO>()).CreateMapper();
        }

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

        public IEnumerable<ProductDTO> GetAll() { return _productMapper.Map<IQueryable<Product>, IEnumerable<ProductDTO>>(_dbcontext.Products.GetAll()); }

        public IEnumerable<ProductDTO> GetByCategory(CategoryDTO category) { return _productMapper.Map<IQueryable<Product>, IEnumerable<ProductDTO>>(_dbcontext.Products.GetByCondition(p => p.CategoryId == category.Id)); }

        public IEnumerable<ProductDTO> GetByVendor(VendorDTO vendor) { return _productMapper.Map<IQueryable<Product>, IEnumerable<ProductDTO>>(_dbcontext.Products.GetByCondition(p => p.VendorId == vendor.Id)); }

        public IEnumerable<ProductDTO> GetWhere(Func<ProductDTO, bool> predicate)
        {
            IEnumerable<ProductDTO> products = _productMapper.Map<IQueryable<Product>, IEnumerable<ProductDTO>>(_dbcontext.Products.GetAll());
            return products.Where(predicate);
        }

        public void RemoveProduct(ProductDTO product)
        {
            Product dproduct = _dbcontext.Products.GetByKey(product.GTIN);
            if (dproduct != null) _dbcontext.Products.Delete(dproduct);

            _dbcontext.SaveChanges();
        }

        public void SetCategory(ProductDTO product, CategoryDTO category)
        {
            Product dproduct = _dbcontext.Products.GetByKey(product.GTIN);
            dproduct.CategoryId = category.Id;
            _dbcontext.Products.Update(dproduct);

            _dbcontext.SaveChanges();
        }

        public void SetVendor(ProductDTO product, VendorDTO vendor)
        {
            Product dproduct = _dbcontext.Products.GetByKey(product.GTIN);
            dproduct.VendorId = vendor.Id;
            _dbcontext.Products.Update(dproduct);

            _dbcontext.SaveChanges();
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}
