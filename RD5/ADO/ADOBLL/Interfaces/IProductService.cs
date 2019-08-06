using System;
using System.Collections.Generic;

using ADOBLL.DTO;

namespace ADOBLL.Interfaces
{
    public interface IProductService : IDisposable
    {
        void RegisterProduct(ProductDTO product, ProductCategoryDTO category = null, VendorDTO vendor = null);
        IEnumerable<ProductDTO> GetProductsByCategoty(ProductCategoryDTO category);
        IEnumerable<ProductDTO> GetProductsOfVendor(VendorDTO vendor);
        IEnumerable<ProductDTO> GetAllProducts();
        void RemoveProduct(ProductDTO product);
    }
}
