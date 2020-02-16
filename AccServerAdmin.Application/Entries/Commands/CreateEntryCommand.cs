using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class CreateEntryCommand : ICreateEntryCommand
    {
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IValidateEntryCommand _validator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEntryCommand(
            IDataRepository<Entry> entryRepository,
            IValidateEntryCommand validator,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }


        public async Task<Entry> Execute(Entry entry)
        {
            await _validator.Execute(entry);
            await _entryRepository.Add(entry);
            await _unitOfWork.SaveChanges();

            return entry;
        }

    }
}