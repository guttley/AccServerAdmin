using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class DeleteGlobalEntryListCommand : IDeleteGlobalEntryListCommand
    {
        private readonly IDataRepository<GlobalEntryList> _entryListRepository;
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGlobalEntryListCommand(
            IDataRepository<GlobalEntryList> entryListRepository,
            IDataRepository<Entry> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryListRepository = entryListRepository;
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid entryId)
        {
            var globalEntry = await _entryListRepository.Get(entryId);

            if (globalEntry.Entries != null)
            {
                foreach (var entry in globalEntry.Entries)
                {
                    _entryRepository.Delete(entry.Id);
                }
            }

            _entryListRepository.Delete(entryId);

            await _unitOfWork.SaveChanges();
        }
    }
}
