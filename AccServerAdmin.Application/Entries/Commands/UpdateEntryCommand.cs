using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;

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


        public async Task ExecuteAsync(Entry entry)
        {
            await _validator.ExecuteAsync(entry).ConfigureAwait(false);
            _entryRepository.Update(entry.Id, entry);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}