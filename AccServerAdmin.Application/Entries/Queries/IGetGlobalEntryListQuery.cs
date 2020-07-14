using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Entries.Queries
{
    public interface IGetGlobalEntryListQuery
    {
        Task<IEnumerable<GlobalEntryList>> Execute();
    }
}
