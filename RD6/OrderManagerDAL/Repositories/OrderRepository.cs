using System;
using System.Linq;

using OrderManagerDAL.Interfaces;
using OrderManagerDAL.Contexts;
using OrderManagerDAL.Models;

namespace OrderManagerDAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationContext _dbcontext;
        public OrderRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(Order entity) { _dbcontext.Orders.Add(entity); }

        public void Delete(Order entity) { _dbcontext.Orders.Remove(entity); }

        public void DeleteByKey(int key)
        {
            Order order = _dbcontext.Orders.Find(key);
            if (order != null) Delete(order);
        }

        public IQueryable<Order> GetAll() {
            IQueryable<Order> orders = _dbcontext.Orders;

            foreach (var order in orders)
                _dbcontext.Entry(order).Collection(o => o.OrderProducts).Load();

            return orders;
        }

        public IQueryable<Order> GetByCondition(Func<Order, bool> predicate) {
            IQueryable<Order> orders = _dbcontext.Orders.Where(predicate).AsQueryable();

            foreach (var order in orders)
                _dbcontext.Entry(order).Collection(o => o.OrderProducts).Load();

            return orders;
        }

        public Order GetByKey(int key) {
            Order order = _dbcontext.Orders.Find(key);

            if (order != null)
                _dbcontext.Entry(order).Collection(p => p.OrderProducts).Load();

            return order;
        }

        public void Update(Order entity) { _dbcontext.Orders.Update(entity); }
    }
}
