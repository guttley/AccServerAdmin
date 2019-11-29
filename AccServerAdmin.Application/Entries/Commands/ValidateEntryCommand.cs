using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class ValidateEntryCommand : IValidateEntryCommand
    {
        private readonly IDataRepository<Entry> _entryRepository;

        public ValidateEntryCommand(
            IDataRepository<Entry> entryRepository)
        {
            _entryRepository = entryRepository;
        }

        private async Task<bool> IsUniqueRaceNumber(Entry entry)
        {
            if (entry.RaceNumber == 0)
                return true;

            var hasMatch = await _entryRepository.GetQueryable()
                                                 .AnyAsync(e => e.RaceNumber == entry.RaceNumber && e.Id != entry.Id)
                                                 .ConfigureAwait(false);
            return !hasMatch;
        }

        private async Task<bool> IsUniqueGridPosition(Entry entry)
        {
            if (entry.DefaultGridPosition == 0)
                return true;

            var hasMatch = await _entryRepository.GetQueryable()
                                                 .AnyAsync(e => e.DefaultGridPosition == entry.DefaultGridPosition && e.Id != entry.Id)
                                                 .ConfigureAwait(false);
            return !hasMatch;
        }


        public async Task ExecuteAsync(Entry entry)
        {
            if (!await IsUniqueRaceNumber(entry).ConfigureAwait(false))
            {
                throw new RaceNumberNotUniqueException($"The race number {entry.RaceNumber} is already in use");
            }

            if (!await IsUniqueGridPosition(entry).ConfigureAwait(false))
            {
                throw new GridPositionNotUniqueException($"The grid position {entry.DefaultGridPosition} is already in use");
            }
        }
    }
}
