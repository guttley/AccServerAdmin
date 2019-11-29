using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IImportEntryListCommand
    {
        Task ExecuteAsync(Guid serverId);
    }
}
