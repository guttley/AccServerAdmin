using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IDeleteEntryCommand
    {
        Task Execute(Guid entryId);
    }
}