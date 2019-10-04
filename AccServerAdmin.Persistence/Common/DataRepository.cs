using AccServerAdmin.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Common
{
    public class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : class, IKeyedEntity
    {
        private readonly ApplicationDbContext _dbContext;


        protected ApplicationDbContext DbContext => _dbContext;

        public DataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual void Update(Guid id, TEntity updated)
        {
            if (updated is null)
                return;

            var existing = _dbContext.Set<TEntity>().Find(id);

            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(updated);
            }
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id).ConfigureAwait(false);
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
