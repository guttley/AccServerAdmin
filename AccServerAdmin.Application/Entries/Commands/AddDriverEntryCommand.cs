using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class AddDriverEntryCommand : IAddDriverEntryCommand
    {
        private readonly IDriverEntryRepository _driverEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddDriverEntryCommand(
            IDriverEntryRepository driverEntryRepository,
            IUnitOfWork unitOfWork)
        {
            _driverEntryRepository = driverEntryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(DriverEntry driverEntry)
        {
            try
            {
                var driverNumber = await _driverEntryRepository
                                         .GetQueryable()
                                         .Where(e => e.EntryId == driverEntry.EntryId)
                                         .MaxAsync(e => e.DriverNumber);

                driverEntry.DriverNumber = ++driverNumber;
            }
            catch (InvalidOperationException)
            {
                driverEntry.DriverNumber = 1;
            }

            await _driverEntryRepository.AddAsync(driverEntry);
            await _unitOfWork.SaveChanges();
        }
    }
}
