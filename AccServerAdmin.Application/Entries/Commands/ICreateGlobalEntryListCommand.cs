using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Entries.Commands
{
    public interface ICreateGlobalEntryListCommand
    {
        Task<GlobalEntryList> Execute(GlobalEntryList entryList);
    } 

}
