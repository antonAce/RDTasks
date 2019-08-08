using EFDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDAL.Configs
{
    /// <summary>
    /// Necessary configuration class for setting up "Vendor" entity
    /// </summary>
    public class VendorConfig : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.ToTable("EF_Vendors")
                .HasKey(v => v.Id).HasName("VendorsPK");

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("vendor_id");

            builder.Property(v => v.Name)
                .IsRequired()
                .HasColumnName("vendor_name")
                .HasColumnType("varchar(250)");

            builder.Property(v => v.Address)
                .HasColumnName("vendor_address")
                .HasColumnType("varchar(250)");
        }
    }
}
