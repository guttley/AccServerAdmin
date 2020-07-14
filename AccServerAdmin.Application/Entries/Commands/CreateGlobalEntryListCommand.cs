using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class CreateGlobalEntryListCommand : ICreateGlobalEntryListCommand
    {
        private readonly IDataRepository<GlobalEntryList> _entryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGlobalEntryListCommand(
            IDataRepository<GlobalEntryList> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<GlobalEntryList> Execute(GlobalEntryList entry)
        {
            await _entryRepository.Add(entry);
            await _unitOfWork.SaveChanges();

            return entry;
        }

    }
}