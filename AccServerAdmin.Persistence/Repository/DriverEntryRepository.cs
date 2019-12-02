﻿using System.Threading.Tasks;
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
            await _dbContext.DriverEntries.AddAsync(driverEntry).ConfigureAwait(false);
        }

        public void Delete(DriverEntry driverEntry)
        {
            _dbContext.DriverEntries.Remove(driverEntry);
        }
    }
}