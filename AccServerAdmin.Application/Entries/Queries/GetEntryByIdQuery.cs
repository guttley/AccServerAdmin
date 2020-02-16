using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Entries.Queries
{
    public class GetEntryByIdQuery : IGetEntryByIdQuery
    {
        private readonly IEntryRepository _entryRepository;

        public GetEntryByIdQuery(
            IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }


        public async Task<Entry> Execute(Guid entryId)
        {
            return await _entryRepository.Get(entryId);
        }

    }
}