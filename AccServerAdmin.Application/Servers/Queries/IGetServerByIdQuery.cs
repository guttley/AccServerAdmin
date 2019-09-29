using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Queries
{
    public interface IGetServerByIdQuery
    {
        Task<Server> ExecuteAsync(Guid serverId);
    }
}
