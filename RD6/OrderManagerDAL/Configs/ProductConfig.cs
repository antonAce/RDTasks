using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OrderManagerDAL.Models;

namespace OrderManagerDAL.Configs
{
    /// <summary>
    /// Essential configuration class for 'Product' entity.
    /// Sets up all column names, types and keys. 
    /// </summary>
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCTS")
                .HasKey(p => p.GTIN)
                .HasName("Products_PK");

            builder.Property(p => p.GTIN)
                .ValueGeneratedNever()
                .HasColumnName("product_GTIN")
                .HasColumnType("varchar(14)");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("product_name")
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Description)
                .HasColumnName("product_description")
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnName("product_price")
                .HasColumnType("money");
        }
    }
}
