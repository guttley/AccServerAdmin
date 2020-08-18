using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class GridResetCommand : IGridResetCommand
    {
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GridResetCommand(
            IDataRepository<Entry> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid entryListId)
        {
            var entries = _entryRepository.GetQueryable().Where(e => e.EntryListId == entryListId);

            foreach (var entry in entries)
            {
                entry.DefaultGridPosition = 0;
            }

            await _unitOfWork.SaveChanges();
        }
    }
}
