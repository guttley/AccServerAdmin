using AccServerAdmin.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Servers.Queries
{
    public interface IGetServerListQuery
    {
        Task<IEnumerable<Server>> ExecuteAsync();
    }
}
