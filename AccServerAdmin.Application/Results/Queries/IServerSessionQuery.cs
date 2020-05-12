using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Results.Queries
{
    public interface IServerSessionQuery
    {
        Task<IEnumerable<Session>> Execute();
    }
}
