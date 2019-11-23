using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Persistence.Repository
{
    public class DriverRepository : DataRepository<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

        public async Task<bool> IsUniqueSteamIdAsync(string steamId)
        {
            return await DbContext.Set<Driver>()
                .AnyAsync(d => d.PlayerId == steamId)
                .ConfigureAwait(false);
        }

    }
}
