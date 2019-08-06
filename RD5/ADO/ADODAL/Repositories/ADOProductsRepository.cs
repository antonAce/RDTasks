using System.Collections.Generic;

using ADODAL.TableGateways;
using ADODAL.Interfaces;
using ADODAL.Models;

namespace ADODAL.Repositories
{
    public class ADOProductsRepository : IProductRepository
    {
        private ProductTableGateway _tableGateway;

        public ADOProductsRepository(ProductTableGateway tableGateway) { _tableGateway = tableGateway; }

        public void Create(Product entity) { _tableGateway.Add(entity); }

        public void Delete(Product entity) { _tableGateway.Delete(entity); }

        public void DeleteByKey(string key) { _tableGateway.DeleteByKey(key); }

        public IEnumerable<Product> GetAll() { return _tableGateway.GetAll(); }

        public Product GetByKey(string key) { return _tableGateway.GetByKey(key); }

        public void Update(Product entity) { _tableGateway.Update(entity); }
    }
}
