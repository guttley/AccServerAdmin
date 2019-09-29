using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccServerAdmin.Persistence.Common
{
    /// <summary>
    /// Generic repository for access data
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IDataRepository<TEntity>
    {
        /// <summary>
        /// Returns all instances of TEntity
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Returns single instance of TEntity
        /// </summary>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Adds a new instance of TEntity, returning the saved record
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing instance of TEntity
        /// </summary>
        Task UpdateAsync(TEntity dbEntity, TEntity entity);

        /// <summary>
        /// Removes an instance of TEntity
        /// </summary>
        Task DeleteAsync(TEntity entity);
    }
}
