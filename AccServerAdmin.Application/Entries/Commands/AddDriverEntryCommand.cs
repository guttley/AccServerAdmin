using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

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
            await _driverEntryRepository.AddAsync(driverEntry).ConfigureAwait(false);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
