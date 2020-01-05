using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.Repository
{
    public interface IDriverRepository : IDataRepository<Driver>
    {
        Task<bool> IsUniqueSteamIdAsync(Driver driver);
    }
}
