using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Entries.Queries
{
    public interface IGetImportableEntriesQuery
    {
        Task<bool> ExecuteAsync(Guid serverId);
    }
}
