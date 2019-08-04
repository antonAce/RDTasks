using System;

using ADODAL.TableGateways;
using ADODAL.Interfaces;

namespace ADODAL.Repositories
{
    public class ADOUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        private ProductCategoryTableGateway _productCategoryTableGateway;
        private ProductTableGateway _productTableGateway;
        private VendorTableGateway _vendorTableGateway;

        private ADOProductCategoriesRepository _ADOProductCategoriesRepository;
        private ADOProductsRepository _ADOProductsRepository;
        private ADOVendorsRepository _ADOVendorsRepository;

        public ADOUnitOfWork(string connectionName)
        {
            _productCategoryTableGateway = new ProductCategoryTableGateway(connectionName);
            _productTableGateway = new ProductTableGateway(connectionName);
            _vendorTableGateway = new VendorTableGateway(connectionName);
        }

        public IProductCategoryRepository ProductCategories
        {
            get
            {
                if (_ADOProductCategoriesRepository == null)
                    _ADOProductCategoriesRepository = new ADOProductCategoriesRepository(_productCategoryTableGateway);
                return _ADOProductCategoriesRepository;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (_ADOProductsRepository == null)
                    _ADOProductsRepository = new ADOProductsRepository(_productTableGateway);
                return _ADOProductsRepository;
            }
        }

        public IVendorRepository Vendors
        {
            get
            {
                if (_ADOVendorsRepository == null)
                    _ADOVendorsRepository = new ADOVendorsRepository(_vendorTableGateway);
                return _ADOVendorsRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _productCategoryTableGateway.Dispose();
                    _productTableGateway.Dispose();
                    _vendorTableGateway.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _productCategoryTableGateway.SaveChanges();
            _productTableGateway.SaveChanges();
            _vendorTableGateway.SaveChanges();
        }
    }
}
