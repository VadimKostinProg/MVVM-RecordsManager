using DataAccess.DBContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        public virtual async Task AddAsync(T entity)
        {
            using ApplicationContext db = new ApplicationContext();

            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            using ApplicationContext db = new ApplicationContext();

            var entity = await db.Set<T>().FindAsync(id);

            if (entity is null) return false;

            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public virtual async Task DeleteRangeAsync(Expression<Func<T, bool>> predicate)
        {
            using ApplicationContext db = new ApplicationContext();

            var entities = db.Set<T>().Where(predicate).ToArray();
            db.Set<T>().RemoveRange(entities);
            await db.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            using ApplicationContext db = new ApplicationContext();

            var entities = db.Set<T>().AsQueryable();

            if (predicate != null)
                entities = entities.Where(predicate);

            return entities.ToList();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            using ApplicationContext db = new ApplicationContext();

            return await db.Set<T>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            using ApplicationContext db = new ApplicationContext();

            db.Set<T>().Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
