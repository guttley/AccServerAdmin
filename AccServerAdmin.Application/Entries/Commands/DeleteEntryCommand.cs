using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class DeleteEntryCommand : IDeleteEntryCommand
    {
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEntryCommand(
            IDataRepository<Entry> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid entryId)
        {
            _entryRepository.Delete(entryId);
            await _unitOfWork.SaveChanges();
        }
    }
}
