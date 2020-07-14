using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;


namespace AccServerAdmin.Application.Entries.Queries
{
    public class GetGlobalEntryListQuery : IGetGlobalEntryListQuery
    {
        private readonly IDataRepository<GlobalEntryList> _entryRepository;

        public GetGlobalEntryListQuery(
            IDataRepository<GlobalEntryList> entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<IEnumerable<GlobalEntryList>> Execute()
        {
            return await _entryRepository.GetAll();
        }

    }
}