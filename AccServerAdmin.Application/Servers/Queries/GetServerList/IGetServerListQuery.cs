using AccServerAdmin.Domain;
using System.Collections.Generic;

namespace AccServerAdmin.Application.Servers.Queries.GetServerList
{
    public interface IGetServerListQuery
    {
        IEnumerable<Server> Execute();
    }
}
