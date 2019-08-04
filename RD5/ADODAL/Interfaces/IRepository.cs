using System.Collections.Generic;

namespace ADODAL.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByKey(TKey key);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteByKey(TKey key);
    }
}
