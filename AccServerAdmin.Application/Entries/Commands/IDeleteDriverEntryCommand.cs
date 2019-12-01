using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IDeleteDriverEntryCommand
    {
        Task ExecuteAsync(DriverEntry driverEntry);
    }
}
