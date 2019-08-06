using System;
using System.Collections.Generic;

using ADOBLL.DTO;

namespace ADOBLL.Interfaces
{
    public interface IProductCategoryService : IDisposable
    {
        void RegisterCategory(ProductCategoryDTO category);
        IEnumerable<ProductCategoryDTO> GetAllCategories();
        void RemoveCategory(ProductCategoryDTO category);
    }
}
