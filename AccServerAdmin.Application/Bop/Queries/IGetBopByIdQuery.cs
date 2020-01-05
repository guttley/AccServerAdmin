using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Bop.Queries
{
    public interface IGetBopByIdQuery
    {
        Task<BalanceOfPerformance> ExecuteAsync(Guid bopId);
    }
}
