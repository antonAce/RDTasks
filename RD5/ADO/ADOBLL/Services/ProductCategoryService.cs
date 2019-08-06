using System;
using System.Linq;
using System.Collections.Generic;

using ADODAL.Interfaces;
using ADODAL.Models;

using ADOBLL.DTO;
using ADOBLL.Interfaces;

using AutoMapper;

namespace ADOBLL.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private IMapper _categoryMapper;

        public ProductCategoryService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            _categoryMapper = new MapperConfiguration(config => config.CreateMap<ProductCategory, ProductCategoryDTO>()).CreateMapper();
        }

        public IEnumerable<ProductCategoryDTO> GetAllCategories()
        {
            return _categoryMapper.Map<IEnumerable<ProductCategory>, List<ProductCategoryDTO>>(UnitOfWork.ProductCategories.GetAll());
        }

        public void RegisterCategory(ProductCategoryDTO category)
        {
            var validationErrors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(category);

            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(category, validationContext, validationErrors, true))
                throw new ArgumentException($"Wrong input data: {string.Join(", ", validationErrors)}");

            UnitOfWork.ProductCategories.Create(new ProductCategory { Id = category.Id, Name = category.Name });
            UnitOfWork.SaveChanges();
        }

        public void RemoveCategory(ProductCategoryDTO category)
        {
            var validationErrors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(category);

            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(category, validationContext, validationErrors, true))
                throw new ArgumentException($"Wrong input data: {string.Join(", ", validationErrors)}");

            UnitOfWork.ProductCategories.Delete(new ProductCategory { Id = category.Id, Name = category.Name });
            UnitOfWork.SaveChanges();
        }

        public void Dispose() { UnitOfWork.Dispose(); }
    }
}
