using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Bop.Queries
{
    public class GetBopByIdQuery : IGetBopByIdQuery
    {
        private readonly IBopRepository _bopRepository;

        public GetBopByIdQuery(IBopRepository bopRepository)
        {
            _bopRepository = bopRepository;
        }
        
        public async Task<BalanceOfPerformance> Execute(Guid driverId)
        {
            return await _bopRepository.Get(driverId).ConfigureAwait(false);
        }
    }
}
