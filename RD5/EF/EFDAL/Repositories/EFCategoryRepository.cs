using System;
using System.Linq;

using EFDAL.Contexts;
using EFDAL.Interfaces;
using EFDAL.Models;

namespace EFDAL.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private ApplicationContext _dbcontext;

        public EFCategoryRepository(ApplicationContext appContext) { _dbcontext = appContext; }

        public void Create(Category entity) { _dbcontext.Categories.Add(entity); }

        public void Delete(Category entity) { _dbcontext.Categories.Remove(entity); }

        public void DeleteByKey(int key)
        {
            Category category = _dbcontext.Categories.Find(key);
            if (category != null) Delete(category);
        }

        public IQueryable<Category> GetAll() { return _dbcontext.Categories; }

        public IQueryable<Category> GetByCondition(Func<Category, bool> predicate) { return _dbcontext.Categories.Where(predicate).AsQueryable(); }

        public Category GetByKey(int key) { return _dbcontext.Categories.Find(key); }

        public void Update(Category entity) { _dbcontext.Categories.Update(entity); }
    }
}
