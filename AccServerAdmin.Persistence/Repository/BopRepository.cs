using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class BopRepository : DataRepository<BalanceOfPerformance>, IBopRepository
    {
        public BopRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

        public async Task<bool> IsUniqueBopAsync(BalanceOfPerformance bop)
        {
            var hasMatch = await DbContext.Set<BalanceOfPerformance>()
                .AnyAsync(b => b.Track == bop.Track && b.Car != bop.Car)
                .ConfigureAwait(false);

            return !hasMatch;
        }
    }
}
