using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IImportEntryListCommand
    {
        Task Execute(Guid serverId);
    }
}
