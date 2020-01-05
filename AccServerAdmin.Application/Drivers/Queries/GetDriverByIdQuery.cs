using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Drivers.Queries
{
    public class GetDriverByIdQuery : IGetDriverByIdQuery
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverByIdQuery(IDriverRepository serverRepository)
        {
            _driverRepository = serverRepository;
        }
        
        public async Task<Driver> ExecuteAsync(Guid driverId)
        {
            return await _driverRepository.GetAsync(driverId).ConfigureAwait(false);
        }
    }
}
