using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
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
        
        public async Task<Driver> Execute(Guid driverId)
        {
            return await _driverRepository.Get(driverId);
        }
    }
}
