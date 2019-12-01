using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IAddDriverEntryCommand
    {
        Task ExecuteAsync(DriverEntry driverEntry);
    }
}
