using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IGridResetCommand
    {
        Task Execute(Guid entryListId);
    }
}
