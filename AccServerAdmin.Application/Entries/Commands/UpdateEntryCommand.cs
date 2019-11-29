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
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEntryCommand(
            IDataRepository<Entry> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task ExecuteAsync(Entry entry)
        {
            if (await _entryRepository.GetQueryable().AnyAsync(e => e.RaceNumber == entry.RaceNumber).ConfigureAwait(false))
            {
                throw new NonUniqueRaceNumberException($"The race number {entry.RaceNumber} is already in use");
            }

            if (entry.RaceNumber > 0 && await _entryRepository.GetQueryable().AnyAsync(e => e.DefaultGridPosition == entry.DefaultGridPosition).ConfigureAwait(false))
            {
                throw new NonUniqueGridPositionException($"The grid position {entry.DefaultGridPosition} is already in use");
            }

            _entryRepository.Update(entry.Id, entry);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}