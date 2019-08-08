using EFDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDAL.Configs
{
    /// <summary>
    /// Not necessary configuration class for "Vendor" entity that adds default data to the DB
    /// </summary>
    public class VendorDefaultDataConfig : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasData(new Vendor[]
                {
                    new Vendor { Id = 1, Name = "Mi Global Store", Address = "ShenZhen City, Guangdong, China" },
                    new Vendor { Id = 2, Name = "ZAIR Official Vendor", Address = "Guangdong China" },
                    new Vendor { Id = 3, Name = "MAOSHUANG Store", Address = "China" },
                    new Vendor { Id = 4, Name = "AsusISonyIHuaweiIMeizuIXiaomiOfficialVendor", Address = "Guangdong China" },
                    new Vendor { Id = 5, Name = "SpeedmotoVendor", Address = "Guangdong China" },
                    new Vendor { Id = 6, Name = "SanDiskSD", Address = "China" },
                    new Vendor { Id = 7, Name = "Ammoon Official", Address = "Guangdong China" },
                    new Vendor { Id = 8, Name = "LiitoKala Official Flagship", Address = "Shenzhen, Guangdong, China" },
                    new Vendor { Id = 9, Name = "Animelover", Address = "China" },
                    new Vendor { Id = 10, Name = "The Dark Knight", Address = "China" },
                    new Vendor { Id = 11, Name = "ANIMORE Official Vendor", Address = "China" }
                });
        }
    }
}
