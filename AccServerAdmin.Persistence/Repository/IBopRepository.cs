using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.Repository
{
    public interface IBopRepository : IDataRepository<BalanceOfPerformance>
    {
        Task<bool> IsUniqueBopAsync(BalanceOfPerformance bop);
    }
}
