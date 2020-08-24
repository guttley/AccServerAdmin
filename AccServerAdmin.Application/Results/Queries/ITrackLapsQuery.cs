using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;

namespace AccServerAdmin.Application.Results.Queries
{
    public interface ITrackLapsQuery
    {
        Task<IList<SessionLap>> Execute(string track);
    }
}
