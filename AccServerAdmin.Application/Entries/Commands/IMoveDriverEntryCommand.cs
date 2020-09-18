using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IMoveDriverEntryCommand
    {
        Task Execute(DriverEntry driverEntry, bool up);
    }
}
