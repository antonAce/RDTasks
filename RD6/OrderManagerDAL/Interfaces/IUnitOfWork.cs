using System;

namespace OrderManagerDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }

        void SaveChanges();
    }
}
