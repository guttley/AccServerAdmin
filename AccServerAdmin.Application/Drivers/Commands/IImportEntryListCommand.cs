using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public interface IImportEntryListCommand
    {
        Task ExecuteAsync(Guid serverId);
    }
}
