using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OrderManagerDAL.Models;

namespace OrderManagerDAL.Configs
{
    /// <summary>
    /// Configuration class for 'Product'-'Order' link entity.
    /// Fills database with startup link data. 
    /// </summary>
    public class OrderProductDefaultDataConfig : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasData(new OrderProduct[] {
                new OrderProduct { OrderId = 5, ProductGTIN = "00032662684547", ProductQuantity = 1 },
                new OrderProduct { OrderId = 5, ProductGTIN = "00033059439610", ProductQuantity = 3 },
                new OrderProduct { OrderId = 3, ProductGTIN = "00032662684547", ProductQuantity = 4 },
                new OrderProduct { OrderId = 4, ProductGTIN = "00032904589723", ProductQuantity = 2 },
                new OrderProduct { OrderId = 5, ProductGTIN = "00032917974724", ProductQuantity = 1 },
                new OrderProduct { OrderId = 1, ProductGTIN = "00032662684547", ProductQuantity = 2 },
                new OrderProduct { OrderId = 1, ProductGTIN = "00032758904354", ProductQuantity = 3 },
                new OrderProduct { OrderId = 3, ProductGTIN = "00033059439610", ProductQuantity = 3 },
                new OrderProduct { OrderId = 7, ProductGTIN = "04000002194089", ProductQuantity = 1 },
                new OrderProduct { OrderId = 4, ProductGTIN = "00033051637203", ProductQuantity = 1 },
                new OrderProduct { OrderId = 2, ProductGTIN = "00033051637203", ProductQuantity = 1 },
                new OrderProduct { OrderId = 7, ProductGTIN = "00033025568455", ProductQuantity = 1 },
                new OrderProduct { OrderId = 1, ProductGTIN = "00033025568455", ProductQuantity = 3 },
                new OrderProduct { OrderId = 7, ProductGTIN = "00032984767921", ProductQuantity = 2 },
                new OrderProduct { OrderId = 6, ProductGTIN = "04000002194089", ProductQuantity = 1 }
            });
        }
    }
}
