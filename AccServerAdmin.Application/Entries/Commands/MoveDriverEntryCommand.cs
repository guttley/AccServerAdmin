using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class MoveDriverEntryCommand : IMoveDriverEntryCommand
    {
        private readonly IDriverEntryRepository _driverEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MoveDriverEntryCommand(
            IDriverEntryRepository driverEntryRepository,
            IUnitOfWork unitOfWork)
        {
            _driverEntryRepository = driverEntryRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task Execute(DriverEntry driverEntry, bool increment)
        {
            var dbEntry = await _driverEntryRepository
                          .GetQueryable()
                          .FirstAsync(e => e.DriverId == driverEntry.DriverId && e.EntryId == driverEntry.EntryId);

            if (dbEntry.DriverNumber == 0)
            {
                dbEntry.DriverNumber = 1;
            }

            var oldNumber = dbEntry.DriverNumber;
            var newNumber = increment
                ? dbEntry.DriverNumber + 1
                : Math.Max(1, dbEntry.DriverNumber - 1);

            var other = await _driverEntryRepository
                              .GetQueryable()
                              .FirstOrDefaultAsync(e => e.EntryId == driverEntry.EntryId && e.DriverNumber == newNumber);

            if (other != null)
            {
                if (increment)
                {
                    other.DriverNumber--;
                }
                else
                {
                    other.DriverNumber++;
                }
            }

            dbEntry.DriverNumber = newNumber;

            await _unitOfWork.SaveChanges();
        }
    }
}
