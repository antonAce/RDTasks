using System;
using System.Collections.Generic;

using EFBLL.DTO;

namespace EFBLL.Interfaces
{
    public interface IProductService : IDisposable
    {
        void AddProduct(ProductDTO product);

        void SetCategory(ProductDTO product, CategoryDTO category);
        void SetVendor(ProductDTO product, VendorDTO vendor);

        IEnumerable<ProductDTO> GetAll();
        IEnumerable<ProductDTO> GetWhere(Func<ProductDTO, bool> predicate);
        IEnumerable<ProductDTO> GetByCategory(CategoryDTO category);
        IEnumerable<ProductDTO> GetByVendor(VendorDTO vendor);

        void RemoveProduct(ProductDTO product);
    }
}
