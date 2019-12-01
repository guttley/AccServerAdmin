using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Persistence.Repository
{
    public interface IDriverEntryRepository
    {
        Task AddAsync(DriverEntry driverEntry);
        void Delete(DriverEntry driverEntry);
    }
}
