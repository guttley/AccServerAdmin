using AccServerAdmin.Domain;
using System.Collections.Generic;

namespace AccServerAdmin.Application.Servers.Queries
{
    public interface IGetServerListQuery
    {
        IEnumerable<Server> Execute();
    }
}
