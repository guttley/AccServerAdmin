using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Persistence.Common
{
    public interface IDriverRepository : IDataRepository<Driver>
    {
        Task<bool> IsUniqueSteamIdAsync(Driver driver);
    }
}
