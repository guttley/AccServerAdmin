using System;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public TEntity DetachEntity<TEntity>(TEntity entity, params string[] idNames) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Detached;

            foreach (var idName in idNames)
            {
                entity.GetType().GetProperty(idName)?.SetValue(entity, Guid.Empty);    
            }

            return entity;
        }
    }
}
