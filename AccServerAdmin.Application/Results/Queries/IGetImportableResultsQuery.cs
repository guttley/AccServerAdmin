using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Results.Queries
{
    public interface IGetImportableResultsQuery
    {
        Task<bool> Execute(Guid serverId);
    }
}
