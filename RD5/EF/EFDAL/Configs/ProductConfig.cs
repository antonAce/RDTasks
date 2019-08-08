using EFDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDAL.Configs
{
    /// <summary>
    /// Necessary configuration class for setting up "Product" entity with all relations
    /// </summary>
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("EF_Products")
                .HasKey(p => p.GTIN).HasName("ProductsPK");

            builder.Property(p => p.GTIN)
                .ValueGeneratedNever()
                .HasColumnName("product_gtin")
                .HasColumnType("varchar(14)");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("product_name")
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Description)
                .HasColumnName("product_description")
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Price)
                .HasColumnName("product_price")
                .HasColumnType("money");

            builder.Property(p => p.CategoryId)
                .HasColumnName("category_id");

            builder.Property(p => p.VendorId)
                .HasColumnName("vendor_id");

            builder.HasOne(p => p.CategoryNav)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.VendorNav)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
