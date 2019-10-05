using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccServerAdmin.Persistence.Common
{
    /// <summary>
    /// Generic repository for access data
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IDataRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Returns a queryable of TEntity
        /// </summary>
        IQueryable<TEntity> GetQueryable();

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
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing instance of TEntity
        /// </summary>
        void Update(Guid id, TEntity entity);

        /// <summary>
        /// Removes an instance of TEntity referenced by the Id
        /// </summary>
        void Delete(Guid id);
    }
}
