using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
       
        protected ApplicationDbContext applicationDbContext;



        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            
            this.applicationDbContext = applicationDbContext;
        }

        public void Add(TEntity entity)
        {
            applicationDbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            applicationDbContext.Set<TEntity>().Remove(entity);
    
        }

        public TEntity Get(int id)
        {            

            return applicationDbContext.Set<TEntity>().Find(id);

        }

        public bool Exists(int id)
        {
            if (applicationDbContext.Set<TEntity>().Find(id) != null)
                return true;
            return false;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return applicationDbContext.Set<TEntity>().AsEnumerable<TEntity>();
        }

        public void Update(TEntity entity)
        {
            applicationDbContext.Entry(entity).State = EntityState.Modified;
            applicationDbContext.Set<TEntity>().Attach(entity);
        }

        public bool Empty()
        {
            return applicationDbContext.Set<TEntity>().Any();
        }



        public void AddInRange(IEnumerable<TEntity> Entities)
        {
            foreach(TEntity entity in Entities)
            {
                applicationDbContext.Add(entity);
            }
        }

        public  TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = applicationDbContext.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

    }

}
