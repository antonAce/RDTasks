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
    public class VendorService : IVendorService
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private IMapper _categoryMapper;
        private IMapper _productMapper;
        private IMapper _vendorMapper;

        public VendorService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _categoryMapper = new MapperConfiguration(config => config.CreateMap<ProductCategory, ProductCategoryDTO>()).CreateMapper();
            _productMapper = new MapperConfiguration(config => config.CreateMap<Product, ProductDTO>()).CreateMapper();
            _vendorMapper = new MapperConfiguration(config => config.CreateMap<Vendor, VendorDTO>()).CreateMapper();
        }

        /// <summary>
        /// Returns set of "vendors", which products have the biggest ammount of different categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VendorDTO> GetMostVariousCategoriesVendors()
        {
            List<VendorDTO> vendors = _vendorMapper.Map<IEnumerable<Vendor>, List<VendorDTO>>(UnitOfWork.Vendors.GetAll());
            List<ProductDTO> products = _productMapper.Map<IEnumerable<Product>, List<ProductDTO>>(UnitOfWork.Products.GetAll());
            var categoriesPerVendor = products.Select(p => new { p.CategoryId, p.VendorId }).Distinct().GroupBy(p => p.VendorId).Select(g => new { VendorId = g.Key, CountOfCategory = g.Count() });
            var vendorsWithMaxCategories = categoriesPerVendor.Where(p => p.CountOfCategory == categoriesPerVendor.Max(c => c.CountOfCategory));

            return vendors.Join(vendorsWithMaxCategories, v => v.Id, vm => vm.VendorId, (v, vm) => v);
        }

        public IEnumerable<VendorDTO> GetVendorsByProductCategoty(ProductCategoryDTO category)
        {
            return GetVendorsByProductCategotyID(category.Id);
        }

        public IEnumerable<VendorDTO> GetVendorsByProductCategotyID(int categoryID)
        {
            Queue<VendorDTO> vendorsDTOoutput = new Queue<VendorDTO>();
            List<ProductDTO> products = _productMapper.Map<IEnumerable<Product>, List<ProductDTO>>(UnitOfWork.Products.GetAll().Where(p => p.CategoryId == categoryID));
            List<VendorDTO> vendors = _vendorMapper.Map<IEnumerable<Vendor>, List<VendorDTO>>(UnitOfWork.Vendors.GetAll());

            var v_result = vendors.Join(products, v => v.Id, p => p.VendorId, (v, p) => new { v.Id, v.Name, v.Address });

            foreach (var vendor in v_result)
                vendorsDTOoutput.Enqueue(new VendorDTO { Id = vendor.Id, Name = vendor.Name, Address = vendor.Address });

            return vendorsDTOoutput;
        }

        public void RegisterVendor(VendorDTO vendor)
        {
            var validationErrors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(vendor);

            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(vendor, validationContext, validationErrors, true))
                throw new ArgumentException($"Wrong input data: {string.Join(", ", validationErrors)}");

            UnitOfWork.Vendors.Create(new Vendor { Id = vendor.Id, Name = vendor.Name, Address = vendor.Address });
            UnitOfWork.SaveChanges();
        }

        public void RemoveVendor(VendorDTO vendor)
        {
            var validationErrors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(vendor);

            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(vendor, validationContext, validationErrors, true))
                throw new ArgumentException($"Wrong input data: {string.Join(", ", validationErrors)}");

            UnitOfWork.Vendors.Delete(new Vendor { Id = vendor.Id, Name = vendor.Name, Address = vendor.Address });
            UnitOfWork.SaveChanges();
        }

        public void Dispose() { UnitOfWork.Dispose(); }

        public IEnumerable<VendorDTO> GetAllVendors()
        {
            return _vendorMapper.Map<IEnumerable<Vendor>, List<VendorDTO>>(UnitOfWork.Vendors.GetAll());
        }
    }
}
