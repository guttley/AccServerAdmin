using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IUpdateEntryCommand
    {
        Task ExecuteAsync(Entry entry);
    }
}