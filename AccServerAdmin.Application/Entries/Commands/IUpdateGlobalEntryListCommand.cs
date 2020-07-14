using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface IUpdateGlobalEntryListCommand
    {
        Task Execute(GlobalEntryList entryList);
    }
}