using System;
using System.Collections.Generic;

using OrderManagerBLL.DTO;

namespace OrderManagerBLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void MakeOrder(OrderDTO order);
        IEnumerable<OrderDTO> ListOrders();
        OrderDTO WatchOrder(int id);
        IEnumerable<ProductDTO> WatchProductsOfOrder(int id);
        void AddProductToOrder(int id, ProductDTO product);
    }
}
