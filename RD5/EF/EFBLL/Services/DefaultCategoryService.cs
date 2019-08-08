using System;
using System.Linq;
using System.Collections.Generic;

using EFDAL.Interfaces;
using EFDAL.Models;

using EFBLL.DTO;
using EFBLL.Interfaces;

using AutoMapper;

namespace EFBLL.Services
{
    public class DefaultCategoryService : ICategoryService
    {
        private IUnitOfWork _dbcontext { get; set; }
        private IMapper _categoryMapper;

        public DefaultCategoryService(IUnitOfWork uow)
        {
            _dbcontext = uow;
            _categoryMapper = new MapperConfiguration(config => config.CreateMap<Category, CategoryDTO>()).CreateMapper();
        }

        public void AddCategory(CategoryDTO category)
        {
            _dbcontext.Categories.Create(new Category { Name = category.Name });
            _dbcontext.SaveChanges();
        }

        public IEnumerable<CategoryDTO> GetAll() { return _categoryMapper.Map<IQueryable<Category>, IEnumerable<CategoryDTO>>(_dbcontext.Categories.GetAll()); }

        public IEnumerable<CategoryDTO> GetWhere(Func<CategoryDTO, bool> predicate)
        {
            IEnumerable<CategoryDTO> categories = _categoryMapper.Map<IQueryable<Category>, IEnumerable<CategoryDTO>>(_dbcontext.Categories.GetAll());
            return categories.Where(predicate);
        }

        public void RemoveCategory(CategoryDTO category)
        {
            Category dcategory = _dbcontext.Categories.GetByKey(category.Id);
            if (dcategory != null) _dbcontext.Categories.Delete(dcategory);

            _dbcontext.SaveChanges();
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}
