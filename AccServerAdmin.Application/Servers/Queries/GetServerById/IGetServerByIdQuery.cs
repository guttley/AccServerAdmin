using AccServerAdmin.Domain;
using System;

namespace AccServerAdmin.Application.Servers.Queries.GetServerList
{
    public interface IGetServerByIdQuery
    {
        Server Execute(Guid serverId);
    }
}
