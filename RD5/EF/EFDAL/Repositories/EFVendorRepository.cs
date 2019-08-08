using System;
using System.Linq;

using EFDAL.Contexts;
using EFDAL.Interfaces;
using EFDAL.Models;


namespace EFDAL.Repositories
{
    public class EFVendorRepository : IVendorRepository
    {
        private ApplicationContext _dbcontext;

        public EFVendorRepository(ApplicationContext appContext) { _dbcontext = appContext; }

        public void Create(Vendor entity) { _dbcontext.Vendors.Add(entity); }

        public void Delete(Vendor entity) { _dbcontext.Vendors.Remove(entity); }

        public void DeleteByKey(int key)
        {
            Vendor product = _dbcontext.Vendors.Find(key);
            if (product != null) Delete(product);
        }

        public IQueryable<Vendor> GetAll() { return _dbcontext.Vendors; }

        public IQueryable<Vendor> GetByCondition(Func<Vendor, bool> predicate) { return _dbcontext.Vendors.Where(predicate).AsQueryable(); }

        public Vendor GetByKey(int key) { return _dbcontext.Vendors.Find(key); }

        public void Update(Vendor entity) { _dbcontext.Vendors.Update(entity); }
    }
}
