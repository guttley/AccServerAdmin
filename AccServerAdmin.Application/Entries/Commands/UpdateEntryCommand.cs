using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class UpdateEntryCommand : IUpdateEntryCommand
    {
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IValidateEntryCommand _validator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEntryCommand(
            IDataRepository<Entry> entryRepository,
            IValidateEntryCommand validator,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }


        public async Task Execute(Entry entry)
        {
            await _validator.Execute(entry).ConfigureAwait(false);
            _entryRepository.Update(entry.Id, entry);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }

    }
}