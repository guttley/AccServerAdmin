using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
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

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>()
                .ToListAsync()
                ;
        }

        public virtual async Task<TEntity> Get(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id)
                ;
        }

        public virtual async Task Add(TEntity entity)
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

        public virtual void Delete(Guid id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
