using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Drivers.Queries
{
    public interface IGetDriverByIdQuery
    {
        Task<Driver> ExecuteAsync(Guid driverId);
    }
}
