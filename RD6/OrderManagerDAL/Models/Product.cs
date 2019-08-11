﻿using System.Collections.Generic;

namespace OrderManagerDAL.Models
{
    public class Product
    {
        public string GTIN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
