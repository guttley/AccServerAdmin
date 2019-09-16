using System;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Queries.GetServerById
{
    public interface IGetServerByIdQuery
    {
        Server Execute(Guid serverId);
    }
}
