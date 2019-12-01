using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Drivers.Commands
{
    using Domain.AccConfig;

    public class CreateDriverCommand : ICreateDriverCommand
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDriverCommand(
            IDriverRepository driverRepository,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Driver> ExecuteAsync(Driver driver)
        {
            if (!await _driverRepository.IsUniqueSteamIdAsync(driver).ConfigureAwait(false))
            {
                throw new SteamIdNotUniqueException("Steam Ids must be unique");
            }

            await _driverRepository.AddAsync(driver);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return driver;
        }
    }
}