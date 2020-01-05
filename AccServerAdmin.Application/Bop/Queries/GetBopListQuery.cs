using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Bop.Queries
{
    public class GetBopListQuery : IGetBopListQuery
    {
        private readonly IBopRepository _bopRepository;

        public GetBopListQuery(IBopRepository bopRepository)
        {
            _bopRepository = bopRepository;
        }

        public async Task<IEnumerable<BalanceOfPerformance>> ExecuteAsync()
        {
            var bop = await _bopRepository.GetAllAsync().ConfigureAwait(false);
            return bop.OrderBy(b => b.Track).ThenBy(b => b.Car);
        }
    }
}
