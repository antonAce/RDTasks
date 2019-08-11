using System.ComponentModel.DataAnnotations;

namespace OrderManagerBLL.DTO
{
    public class ProductDTO
    {
        public string GTIN { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
