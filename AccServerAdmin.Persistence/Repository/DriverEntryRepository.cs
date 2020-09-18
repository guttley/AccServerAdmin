using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.DbContext;

namespace AccServerAdmin.Persistence.Repository
{
    public class DriverEntryRepository : IDriverEntryRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public DriverEntryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(DriverEntry driverEntry)
        {
            await _dbContext.DriverEntries.AddAsync(driverEntry);
        }

        public void Delete(DriverEntry driverEntry)
        {
            _dbContext.DriverEntries.Remove(driverEntry);
        }

        /// <inheritdoc />
        public void Update(DriverEntry driverEntry)
        {
            if (driverEntry is null)
                return;

            var existing = _dbContext.Set<DriverEntry>().Find(driverEntry.EntryId, driverEntry.DriverId);

            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(driverEntry);
            }
        }

        public IQueryable<DriverEntry> GetQueryable()
        {
            return _dbContext.DriverEntries;
        }
    }
}
