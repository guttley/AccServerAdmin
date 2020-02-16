using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public class DeleteDriverCommand : IDeleteDriverCommand
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IDataRepository<SessionDriver> _sessionDriverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriverCommand(
            IDriverRepository driverRepository,
            IDataRepository<SessionDriver> sessionDriverRepository,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _sessionDriverRepository = sessionDriverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid driverId)
        {
            var anonDriver = await _driverRepository.Get(ListData.AnonymousDriverId);
            var sessionDrivers = _sessionDriverRepository.GetQueryable().Where(sd => sd.Driver.Id == driverId).ToList();
            sessionDrivers.ForEach(sd => sd.Driver = anonDriver);

            _driverRepository.Delete(driverId);
            await _unitOfWork.SaveChanges();
        }
    }
}
