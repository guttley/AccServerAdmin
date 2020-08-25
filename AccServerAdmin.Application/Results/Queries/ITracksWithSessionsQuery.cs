using System.Collections.Generic;
using System.Threading.Tasks;


namespace AccServerAdmin.Application.Results.Queries
{
    public interface ITracksWithSessionsQuery
    {
        Task<IList<string>> Execute(int daysHistory);
    }
}
