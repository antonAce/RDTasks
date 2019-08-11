using OrderManagerDAL.Models;

namespace OrderManagerDAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order, int> { }
}
