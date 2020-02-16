using System.Threading.Tasks;

namespace AccServerAdmin.Persistence.Common
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
