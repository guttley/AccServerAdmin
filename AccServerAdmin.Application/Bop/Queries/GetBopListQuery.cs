using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Bop.Queries
{
    public class GetBopListQuery : IGetBopListQuery
    {
        private readonly IBopRepository _bopRepository;

        public GetBopListQuery(IBopRepository bopRepository)
        {
            _bopRepository = bopRepository;
        }

        public async Task<IEnumerable<BalanceOfPerformance>> Execute(Guid serverId)
        {
            return await _bopRepository.GetQueryable()
                .Where(b => b.ServerId == serverId)
                .OrderBy(b => b.Track)
                .ThenBy(b => b.Car)
                .ToListAsync()
                ;
        }
    }
}
