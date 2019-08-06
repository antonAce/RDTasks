using System.ComponentModel.DataAnnotations;

namespace ADOBLL.DTO
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
