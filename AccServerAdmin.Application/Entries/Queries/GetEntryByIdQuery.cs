using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Queries
{
    public class GetEntryByIdQuery : IGetEntryByIdQuery
    {
        private readonly IDataRepository<Entry> _entryRepository;

        public GetEntryByIdQuery(
            IDataRepository<Entry> entryRepository)
        {
            _entryRepository = entryRepository;
        }


        public async Task<Entry> ExecuteAsync(Guid entryId)
        {
            return await _entryRepository.GetAsync(entryId).ConfigureAwait(false);
        }

    }
}