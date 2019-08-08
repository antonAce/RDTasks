using System;
using System.Collections.Generic;

using EFBLL.DTO;

namespace EFBLL.Interfaces
{
    public interface IVendorService : IDisposable
    {
        void AddVendor(VendorDTO vendor);

        IEnumerable<VendorDTO> GetAll();
        IEnumerable<VendorDTO> GetWhere(Func<VendorDTO, bool> predicate);
        IEnumerable<VendorDTO> GetByCategory(CategoryDTO category);

        void RemoveVendor(VendorDTO vendor);
    }
}
