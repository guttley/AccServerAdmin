using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Drivers.Queries
{
    public interface IGetDriverListQuery
    {
        Task<IEnumerable<Driver>> ExecuteAsync();
    }
}
