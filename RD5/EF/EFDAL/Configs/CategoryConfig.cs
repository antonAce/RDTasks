using EFDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDAL.Configs
{
    /// <summary>
    /// Necessary configuration class for setting up "Category" entity
    /// </summary>
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("EF_ProductCategories")
                .HasKey(c => c.Id).HasName("ProductCategoriesPK");

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("category_id");

            builder.Property(c => c.Name)
                .IsRequired().HasColumnName("category_name")
                .HasColumnType("varchar(250)");
        }
    }
}
