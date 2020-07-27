using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public class DeleteDriverCommand : IDeleteDriverCommand
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IDataRepository<SessionCarDriver> _sessionDriverRepository;
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IDriverEntryRepository _driverEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriverCommand(
            IDriverRepository driverRepository,
            IDataRepository<SessionCarDriver> sessionDriverRepository,
            IDataRepository<Entry> entryRepository,
            IDriverEntryRepository driverEntryRepository,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _sessionDriverRepository = sessionDriverRepository;
            _entryRepository = entryRepository;
            _driverEntryRepository = driverEntryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid driverId)
        {
            //var anonDriver = await _driverRepository.Get(ListData.AnonymousDriverId);
            var entries = _driverEntryRepository.GetQueryable().Where(e => e.DriverId == driverId);

            foreach (var driver in entries)
            {
                _entryRepository.Delete(driver.Entry);
                _driverEntryRepository.Delete(driver);
            }

            await _unitOfWork.SaveChanges();

            var sessionDrivers = _sessionDriverRepository.GetQueryable().Where(sd => sd.DriverId == driverId).ToList();

            foreach (var driver in sessionDrivers)
            {
                _sessionDriverRepository.Delete(driver);
            }

            await _unitOfWork.SaveChanges();
            _driverRepository.Delete(driverId);
            await _unitOfWork.SaveChanges();
        }
    }
}
