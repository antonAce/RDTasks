using System.Collections.Generic;

using ADODAL.TableGateways;
using ADODAL.Interfaces;
using ADODAL.Models;

namespace ADODAL.Repositories
{
    public class ADOProductCategoriesRepository : IProductCategoryRepository
    {
        private ProductCategoryTableGateway _tableGateway;

        public ADOProductCategoriesRepository(ProductCategoryTableGateway tableGateway) { _tableGateway = tableGateway; }

        public void Create(ProductCategory entity) { _tableGateway.Add(entity); }

        public void Delete(ProductCategory entity) { _tableGateway.Delete(entity); }

        public void DeleteByKey(int key) { _tableGateway.DeleteByKey(key); }

        public IEnumerable<ProductCategory> GetAll() { return _tableGateway.GetAll(); }

        public ProductCategory GetByKey(int key) { return _tableGateway.GetByKey(key); }

        public void Update(ProductCategory entity) { _tableGateway.Update(entity); }
    }
}
