using System.Collections.Generic;

namespace EFDAL.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
