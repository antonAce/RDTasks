namespace EFDAL.Models
{
    public class Product
    {
        public string GTIN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public int? VendorId { get; set; }
        public virtual Vendor VendorNav { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category CategoryNav { get; set; }
    }
}
