using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public class UpdateDriverCommand : IUpdateDriverCommand
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDriverCommand(
            IDriverRepository driverRepository,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Driver driver)
        {
            if (!await _driverRepository.IsUniqueSteamIdAsync(driver).ConfigureAwait(false))
            {
                throw new SteamIdNotUniqueException("Steam Ids must be unique");
            }

            _driverRepository.Update(driver.Id, driver);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
