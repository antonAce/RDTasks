using System;
using System.Linq;
using System.Collections.Generic;

using OrderManagerDAL.Interfaces;
using OrderManagerDAL.Models;

using OrderManagerBLL.DTO;
using OrderManagerBLL.Interfaces;

namespace OrderManagerBLL.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _dbcontext;

        public OrderService(IUnitOfWork unitOfWork) { _dbcontext = unitOfWork; }

        public void AddProductToOrder(int id, ProductDTO product)
        {
            Order order = _dbcontext.Orders.GetByKey(id);

            if (order != null)
            {
                var orderproduct = order.OrderProducts.Where(op => op.OrderId == id && op.ProductGTIN == product.GTIN).FirstOrDefault();

                if (orderproduct == null)
                    order.OrderProducts.Add(new OrderProduct { OrderId = id, ProductGTIN = product.GTIN });
                else
                    orderproduct.ProductQuantity += 1;
            }

            _dbcontext.SaveChanges();
        }

        public IEnumerable<OrderDTO> ListOrders()
        {
            IEnumerable<Order> orders = _dbcontext.Orders.GetAll();

            foreach (Order order in orders)
                yield return new OrderDTO { Id = order.Id, OrderingDate = order.OrderingDate };
        }

        public void MakeOrder(OrderDTO order)
        {
            _dbcontext.Orders.Create(new Order { OrderingDate = DateTime.Now });
            _dbcontext.SaveChanges();
        }

        public OrderDTO WatchOrder(int id)
        {
            Order order = _dbcontext.Orders.GetByKey(id);

            if (order != null)
                return new OrderDTO { Id = order.Id, OrderingDate = order.OrderingDate };
            else
                return null;
        }

        public IEnumerable<ProductDTO> WatchProductsOfOrder(int id)
        {
            Order order = _dbcontext.Orders.GetByKey(id);

            if (order != null)
            {
                foreach (var op in order.OrderProducts)
                {
                    Product product = op.ProductNav;

                    for (int i = 0; i < op.ProductQuantity; i++)
                    {
                        yield return new ProductDTO
                        {
                            GTIN = product.GTIN,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price
                        };
                    }
                }
            }
            else
                yield break;
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}
