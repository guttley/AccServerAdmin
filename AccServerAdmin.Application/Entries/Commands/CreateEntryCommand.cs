using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class CreateEntryCommand : ICreateEntryCommand
    {
        private readonly IDataRepository<Entry> _entryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEntryCommand(
            IDataRepository<Entry> entryRepository,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Entry> ExecuteAsync(Entry entry)
        {
            if (await _entryRepository.GetQueryable().AnyAsync(e => e.RaceNumber == entry.RaceNumber).ConfigureAwait(false))
            {
                throw new NonUniqueRaceNumberException($"The race number {entry.RaceNumber} is already in use");
            }

            if (entry.RaceNumber > 0 && await _entryRepository.GetQueryable().AnyAsync(e => e.DefaultGridPosition == entry.DefaultGridPosition).ConfigureAwait(false))
            {
                throw new NonUniqueGridPositionException($"The grid position {entry.DefaultGridPosition} is already in use");
            }

            await _entryRepository.AddAsync(entry).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return entry;
        }

    }
}