using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class UpdateGlobalEntryListCommand : IUpdateGlobalEntryListCommand
    {
        private readonly IDataRepository<GlobalEntryList> _entryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGlobalEntryListCommand(
            IDataRepository<GlobalEntryList> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(GlobalEntryList entry)
        {
            _entryRepository.Update(entry.Id, entry);
            await _unitOfWork.SaveChanges();
        }

    }
}