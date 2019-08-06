using System.ComponentModel.DataAnnotations;

namespace ADOBLL.DTO
{
    public class VendorDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; }
    }
}
