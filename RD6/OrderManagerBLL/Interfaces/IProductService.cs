using System;
using System.Collections.Generic;

using OrderManagerBLL.DTO;

namespace OrderManagerBLL.Interfaces
{
    public interface IProductService : IDisposable
    {
        void AddProduct(ProductDTO product);
        IEnumerable<ProductDTO> ListProducts();
        ProductDTO GetProductByGTIN(string GTIN);
    }
}
