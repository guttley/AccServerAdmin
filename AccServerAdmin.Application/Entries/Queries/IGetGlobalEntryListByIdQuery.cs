using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Entries.Queries
{
    public interface IGetGlobalEntryListByIdQuery
    {
        Task<GlobalEntryList> Execute(Guid entryId);
    }
}