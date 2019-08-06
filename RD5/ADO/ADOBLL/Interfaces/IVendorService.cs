using System;
using System.Collections.Generic;

using ADOBLL.DTO;

namespace ADOBLL.Interfaces
{
    public interface IVendorService : IDisposable
    {
        void RegisterVendor(VendorDTO vendor);
        IEnumerable<VendorDTO> GetAllVendors();
        IEnumerable<VendorDTO> GetVendorsByProductCategoty(ProductCategoryDTO category);
        IEnumerable<VendorDTO> GetVendorsByProductCategotyID(int categoryID);
        IEnumerable<VendorDTO> GetMostVariousCategoriesVendors();
        void RemoveVendor(VendorDTO vendor);
    }
}
