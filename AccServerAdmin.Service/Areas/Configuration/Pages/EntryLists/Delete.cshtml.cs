using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.EntryLists
{
    public class DeleteEntryListModel : PageModel
    {
        private readonly IGetGlobalEntryListByIdQuery _getByIdQuery;
        private readonly IDeleteGlobalEntryListCommand _deleteCommand;

        public DeleteEntryListModel(
            IGetGlobalEntryListByIdQuery getByIdQuery,
            IDeleteGlobalEntryListCommand deleteCommand)
        {
            _getByIdQuery = getByIdQuery;
            _deleteCommand = deleteCommand;
        }

        [BindProperty]
        public GlobalEntryList EntryList { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            EntryList = await _getByIdQuery.Execute(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteCommand.Execute(EntryList.Id);
            return RedirectToPage("./List");
        }

    }
}

