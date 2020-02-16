using System;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public class DeleteDriverCommand : IDeleteDriverCommand
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriverCommand(
            IDriverRepository driverRepository,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid driverId)
        {
            _driverRepository.Delete(driverId);
            await _unitOfWork.SaveChanges();
        }
    }
}
