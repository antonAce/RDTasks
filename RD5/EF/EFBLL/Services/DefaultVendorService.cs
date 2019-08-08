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
    public class DefaultVendorService : IVendorService
    {
        private IUnitOfWork _dbcontext { get; set; }
        private IMapper _vendorMapper;

        public DefaultVendorService(IUnitOfWork uow)
        {
            _dbcontext = uow;
            _vendorMapper = new MapperConfiguration(config => config.CreateMap<Vendor, VendorDTO>()).CreateMapper();
        }

        public void AddVendor(VendorDTO vendor)
        {
            _dbcontext.Vendors.Create(new Vendor { Name = vendor.Name, Address = vendor.Address });
            _dbcontext.SaveChanges();
        }

        public IEnumerable<VendorDTO> GetAll() { return _vendorMapper.Map<IQueryable<Vendor>, IEnumerable<VendorDTO>>(_dbcontext.Vendors.GetAll()); }

        public IEnumerable<VendorDTO> GetByCategory(CategoryDTO category)
        {
            IQueryable<Product> productsOfRequiredCategory = _dbcontext.Products.GetAll().Where(p => p.CategoryId == category.Id);
            return _dbcontext.Vendors.GetAll().Join(productsOfRequiredCategory,
                v => v.Id, 
                p => p.VendorId, 
                (v, p) => new VendorDTO { Id = v.Id, Name = v.Name, Address = v.Address }).Distinct();
        }

        public IEnumerable<VendorDTO> GetWhere(Func<VendorDTO, bool> predicate) {
            IEnumerable<VendorDTO> vendors = _vendorMapper.Map<IQueryable<Vendor>, IEnumerable<VendorDTO>>(_dbcontext.Vendors.GetAll());
            return vendors.Where(predicate);
        }

        public void RemoveVendor(VendorDTO vendor)
        {
            Vendor dVendor = _dbcontext.Vendors.GetByKey(vendor.Id);
            if (dVendor != null) _dbcontext.Vendors.Delete(dVendor);

            _dbcontext.SaveChanges();
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}
