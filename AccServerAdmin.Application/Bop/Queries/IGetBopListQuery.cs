using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Bop.Queries
{
    public interface IGetBopListQuery
    {
        Task<IEnumerable<BalanceOfPerformance>> Execute(Guid serverId);
    }
}
