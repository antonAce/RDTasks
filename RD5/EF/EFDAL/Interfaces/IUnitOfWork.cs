using System;

namespace EFDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IVendorRepository Vendors { get; }

        void SaveChanges();
    }
}
