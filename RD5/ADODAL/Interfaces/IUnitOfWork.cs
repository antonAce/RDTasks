using System;

namespace ADODAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductCategoryRepository ProductCategories { get; }
        IProductRepository Products { get; }
        IVendorRepository Vendors { get; }

        void SaveChanges();
    }
}
