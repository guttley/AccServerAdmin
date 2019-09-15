using AccServerAdmin.Domain;
using System;

namespace AccServerAdmin.Application.Servers.Queries.GetServerList
{
    public class GetServerByIdQuery : IGetServerByIdQuery
    {
        public Server Execute(Guid serverId)
        {
            throw new NotImplementedException();
        }
    }
}
