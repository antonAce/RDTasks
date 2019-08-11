using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OrderManagerDAL.Models;

namespace OrderManagerDAL.Configs
{
    /// <summary>
    /// Essential configuration class for MANY-MANY link entity between 'Order' and 'Product'.
    /// Sets up all column names, types, keys and auto-compute fields.
    /// </summary>
    public class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("ORDER_TO_PRODUCT")
                .HasKey(op => new { op.OrderId, op.ProductGTIN })
                .HasName("OrderToProduct_PK");

            builder.Property(op => op.OrderId)
                .HasColumnName("order_id");

            builder.Property(op => op.ProductGTIN)
                .HasColumnName("product_GTIN")
                .HasColumnType("varchar(14)");

            builder.Property(op => op.ProductQuantity)
                .HasColumnName("product_quantity");

            builder.HasOne(op => op.OrderNav)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.ProductNav)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductGTIN);
        }
    }
}
