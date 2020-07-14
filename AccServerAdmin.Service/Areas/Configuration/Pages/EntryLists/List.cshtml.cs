using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.EntryLists
{
    public class EntryListModel : PageModel
    {
        private readonly IGetGlobalEntryListQuery _getListQuery;

        public EntryListModel(IGetGlobalEntryListQuery getListQuery)
        {
            _getListQuery = getListQuery;
        }

        [BindProperty]
        public IEnumerable<GlobalEntryList> EntryLists { get; set; }

        public async Task OnGetAsync()
        {
            EntryLists = await _getListQuery.Execute();
        }
    }
}
