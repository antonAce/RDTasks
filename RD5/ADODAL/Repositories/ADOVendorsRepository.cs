using System.Collections.Generic;

using ADODAL.TableGateways;
using ADODAL.Interfaces;
using ADODAL.Models;

namespace ADODAL.Repositories
{
    public class ADOVendorsRepository : IVendorRepository
    {
        private VendorTableGateway _tableGateway;

        public ADOVendorsRepository(VendorTableGateway tableGateway) { _tableGateway = tableGateway; }

        public void Create(Vendor entity) { _tableGateway.Add(entity); }

        public void Delete(Vendor entity) { _tableGateway.Delete(entity); }

        public void DeleteByKey(int key) { _tableGateway.DeleteByKey(key); }

        public IEnumerable<Vendor> GetAll() { return _tableGateway.GetAll(); }

        public Vendor GetByKey(int key) { return _tableGateway.GetByKey(key); }

        public void Update(Vendor entity) { _tableGateway.Update(entity); }
    }
}
