using System;
using System.Collections.Generic;

namespace OrderManagerDAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderingDate { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
