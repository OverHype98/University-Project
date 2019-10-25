using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UniversityProject.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
       
            
            TEntity Get(int id);   //gets entity by id    
            IEnumerable<TEntity> GetAll(); //gets all entities
            TEntity GetFirstOrDefault(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includes);
            void Add(TEntity entity);
            void Delete(TEntity entity);
            void Update(TEntity entity);
            void AddInRange(IEnumerable<TEntity> entities);
            bool Exists(int id);

            bool Empty();
    }
}
