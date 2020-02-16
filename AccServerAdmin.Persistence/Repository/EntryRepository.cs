using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;


namespace AccServerAdmin.Persistence.Repository
{
    public class EntryRepository : DataRepository<Entry>, IEntryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EntryRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Entry> Get(Guid id)
        {
            return await _dbContext.Set<Entry>()
                .Include(e => e.Entries).ThenInclude(e => e.Driver)
                .FirstOrDefaultAsync(e => e.Id == id)
                ;
        }

        public override async Task<IEnumerable<Entry>> GetAll()
        {
            return await _dbContext.Set<Entry>()
                .Include(e => e.Entries).ThenInclude(e => e.Driver)
                .ToListAsync()
                ;
        }
    }
}
