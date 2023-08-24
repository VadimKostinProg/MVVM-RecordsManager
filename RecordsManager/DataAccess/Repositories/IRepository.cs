using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Data access logic tools.
    /// </summary>
    /// <typeparam name="T">Entity of the data base.</typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Method for reading the entity value from the data base by it`s Id.
        /// </summary>
        /// <param name="id">Guid of the entity to read.</param>
        /// <returns>Entity with passed Id, null - if entity is not found.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Method for reading all entities from the data base.
        /// </summary>
        /// <param name="predicate">Expression to filter entities.</param>
        /// <returns>Collection IEnumerable of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);

        /// <summary>
        /// Method for adding new entity to the data base.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Method to update existing entity in the data base.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Method for deleting entity from the data base.
        /// </summary>
        /// <param name="id">Guid of entity to delete.</param>
        /// <returns>True - if entity is successfully deleted, otherwise - false.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Method for deleting range of entities filtered by passed predicate.
        /// </summary>
        /// <param name="predicate">Expression to filter entities to delete.</param>
        Task DeleteRangeAsync(Expression<Func<T, bool>> predicate);
    }
}
