using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class DeleteDriverEntryCommand : IDeleteDriverEntryCommand
    {
        private readonly IDriverEntryRepository _driverEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriverEntryCommand(
            IDriverEntryRepository driverEntryRepository,
            IUnitOfWork unitOfWork)
        {
            _driverEntryRepository = driverEntryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(DriverEntry driverEntry)
        {
            _driverEntryRepository.Delete(driverEntry);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
