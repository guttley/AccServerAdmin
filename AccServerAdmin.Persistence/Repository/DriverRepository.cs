using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
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

        public async Task<bool> IsUniqueSteamIdAsync(Driver driver)
        {
            var hasMatch = await DbContext.Set<Driver>()
                .AnyAsync(d => d.PlayerId == driver.PlayerId && d.Id != driver.Id)
                .ConfigureAwait(false);

            return !hasMatch;
        }

    }
}
