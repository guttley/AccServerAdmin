using System.Threading.Tasks;

namespace AccServerAdmin.Persistence.Common
{
    public interface IUnitOfWork
    {
        Task SaveChanges();

        /// <summary>
        /// Detaches the entity from the DB
        /// </summary>
        TEntity DetachEntity<TEntity>(TEntity entity, params string[] idNames) where TEntity : class;
    }
}
