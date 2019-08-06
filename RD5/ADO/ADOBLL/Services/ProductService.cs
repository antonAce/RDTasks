using System;
using System.Linq;
using System.Collections.Generic;

using ADODAL.Interfaces;
using ADODAL.Models;

using ADOBLL.DTO;
using ADOBLL.Interfaces;

using AutoMapper;

namespace ADOBLL.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private IMapper _categoryMapper;
        private IMapper _productMapper;
        private IMapper _vendorMapper;

        public ProductService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _categoryMapper = new MapperConfiguration(config => config.CreateMap<ProductCategory, ProductCategoryDTO>()).CreateMapper();
            _productMapper = new MapperConfiguration(config => config.CreateMap<Product, ProductDTO>()).CreateMapper();
            _vendorMapper = new MapperConfiguration(config => config.CreateMap<Vendor, VendorDTO>()).CreateMapper();
        }

        public IEnumerable<ProductDTO> GetProductsByCategoty(ProductCategoryDTO category)
        {
            return _productMapper.Map<IEnumerable<Product>, List<ProductDTO>>(UnitOfWork.Products.GetAll().Where(p => p.CategoryId == category.Id));
        }

        public IEnumerable<ProductDTO> GetProductsOfVendor(VendorDTO vendor)
        {
            return _productMapper.Map<IEnumerable<Product>, List<ProductDTO>>(UnitOfWork.Products.GetAll().Where(p => p.VendorId == vendor.Id));
        }

        /// <summary>
        /// Registers product into two different ways:
        /// 1) By all values of "product" (including reference value, like foreign keys), if two last params leave optional.
        /// 2) By all non-reference values of "product" and all "category" and "vendor" values
        /// </summary>
        /// <param name="product">Product to add</param>
        /// <param name="category">Optional param of category which product is related</param>
        /// <param name="vendor">Optional param of vendor which product is related</param>
        public void RegisterProduct(ProductDTO product, ProductCategoryDTO category = null, VendorDTO vendor = null)
        {
            UnitOfWork.Products.Create(new Product {
                GTIN = product.GTIN,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = (category == null) ? product.CategoryId : category.Id,
                VendorId = (vendor == null) ? product.VendorId : vendor.Id,
            });

            UnitOfWork.SaveChanges();
        }

        public void RemoveProduct(ProductDTO product)
        {
            var validationErrors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(product);

            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(product, validationContext, validationErrors, true))
                throw new ArgumentException($"Wrong input data: {string.Join(", ", validationErrors)}");

            UnitOfWork.Products.Delete(new Product {
                GTIN = product.GTIN,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                VendorId = product.VendorId
            });

            UnitOfWork.SaveChanges();
        }

        public void Dispose() { UnitOfWork.Dispose(); }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _productMapper.Map<IEnumerable<Product>, List<ProductDTO>>(UnitOfWork.Products.GetAll());
        }
    }
}
