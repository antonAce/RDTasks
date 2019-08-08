using System;
using System.Collections.Generic;

using EFBLL.DTO;

namespace EFBLL.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        void AddCategory(CategoryDTO category);

        IEnumerable<CategoryDTO> GetAll();
        IEnumerable<CategoryDTO> GetWhere(Func<CategoryDTO, bool> predicate);

        void RemoveCategory(CategoryDTO category);
    }
}
