using System.ComponentModel.DataAnnotations;

namespace ADOBLL.DTO
{
    public class ProductDTO
    {
        // Global Trade Item Number
        [Required]
        [StringLength(14)]
        public string GTIN { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int VendorId { get; set; }
    }
}
