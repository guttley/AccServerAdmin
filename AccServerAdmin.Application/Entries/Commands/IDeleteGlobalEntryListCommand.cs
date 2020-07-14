using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IDeleteGlobalEntryListCommand
    {
        Task Execute(Guid entryId);
    }
}