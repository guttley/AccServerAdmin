using System;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Queries
{
    public interface IGetServerByIdQuery
    {
        Server Execute(Guid serverId);
    }
}
