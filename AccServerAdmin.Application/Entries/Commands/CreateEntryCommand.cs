﻿using System.Threading.Tasks;
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


        public async Task<Entry> ExecuteAsync(Entry entry)
        {
            await _validator.ExecuteAsync(entry).ConfigureAwait(false);
            await _entryRepository.AddAsync(entry).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return entry;
        }

    }
}