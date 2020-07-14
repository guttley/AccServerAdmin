using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Entries.Queries
{
    public class GetGlobalEntryListByIdQuery : IGetGlobalEntryListByIdQuery
    {
        private readonly IDataRepository<GlobalEntryList> _entryRepository;

        public GetGlobalEntryListByIdQuery(IDataRepository<GlobalEntryList> entryRepository)
        {
            _entryRepository = entryRepository;
        }

        Task<GlobalEntryList> IGetGlobalEntryListByIdQuery.Execute(Guid entryId)
        {
            return _entryRepository.Get(entryId);
        }
    }
}