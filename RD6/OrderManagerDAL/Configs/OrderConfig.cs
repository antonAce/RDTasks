using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OrderManagerDAL.Models;

namespace OrderManagerDAL.Configs
{
    /// <summary>
    /// Essential configuration class for 'Order' entity.
    /// Sets up all column names, types, keys and auto-compute fields.
    /// </summary>
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("ORDERS")
                .HasKey(o => o.Id)
                .HasName("Orders_PK");

            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("order_id");

            builder.Property(o => o.OrderingDate)
                .IsRequired()
                .HasColumnName("order_date")
                .HasColumnType("date");
        }
    }
}
