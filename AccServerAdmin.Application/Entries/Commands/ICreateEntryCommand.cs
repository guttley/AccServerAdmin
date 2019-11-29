using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface ICreateEntryCommand
    {
        Task<Entry> ExecuteAsync(Entry entry);
    }
}