using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Queries
{
    public interface IGetEntryByIdQuery
    {
        Task<Entry> Execute(Guid entryId);
    }
}