using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OrderManagerDAL.Models;

namespace OrderManagerDAL.Configs
{
    /// <summary>
    /// Configuration class for 'Order' entity.
    /// Fills database with startup data. 
    /// </summary>
    public class OrderDefaultDataConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(new Order[] {
                new Order { Id = 1, OrderingDate = new DateTime(2016, 9, 21) },
                new Order { Id = 2, OrderingDate = new DateTime(2016, 10, 12) },
                new Order { Id = 3, OrderingDate = new DateTime(2015, 1, 5) },
                new Order { Id = 4, OrderingDate = new DateTime(2017, 3, 12) },
                new Order { Id = 5, OrderingDate = new DateTime(2016, 3, 14) },
                new Order { Id = 6, OrderingDate = new DateTime(2016, 11, 4) },
                new Order { Id = 7, OrderingDate = new DateTime(2018, 2, 27) }
            });
        }
    }
}
