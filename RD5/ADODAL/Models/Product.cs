namespace ADODAL.Models
{
    public class Product
    {
        // Global Trade Item Number
        public string GTIN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public int CategoryId { get; set; }
        public int VendorId { get; set; }
    }
}
