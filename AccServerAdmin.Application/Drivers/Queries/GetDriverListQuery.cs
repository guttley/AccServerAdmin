using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Drivers.Queries
{
    public class GetDriverListQuery : IGetDriverListQuery
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverListQuery(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<IEnumerable<Driver>> ExecuteAsync()
        {
            var drivers = await _driverRepository.GetAllAsync().ConfigureAwait(false);
            return drivers.OrderBy(d => d.Fullname);
        }
    }
}
