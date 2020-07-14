using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.EntryLists
{
    public class EditEntryListModel : PageModel
    {
        private readonly IGetGlobalEntryListByIdQuery _getByIdQuery;
        private readonly ICreateGlobalEntryListCommand _createCommand;
        private readonly IUpdateGlobalEntryListCommand _updateCommand;

        public EditEntryListModel(
            IGetGlobalEntryListByIdQuery getByIdQuery,
            ICreateGlobalEntryListCommand createCommand,
            IUpdateGlobalEntryListCommand updateCommand)
        {
            _getByIdQuery = getByIdQuery;
            _createCommand = createCommand;
            _updateCommand = updateCommand;
        }

        [BindProperty]
        public GlobalEntryList EntryList { get; set; }


        public async Task OnGetAsync(Guid id)
        {
            EntryList = id == Guid.Empty
                     ? new GlobalEntryList() 
                     : await _getByIdQuery.Execute(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (EntryList.Id == Guid.Empty)
                    {
                        await _createCommand.Execute(EntryList);
                    } 
                    else 
                    {
                        await _updateCommand.Execute(EntryList);
                    }
                }
                catch (SteamIdNotUniqueException nex)
                {
                    ModelState.AddModelError("EntryList.Id", nex.Message);
                }
                catch (EmptyDirectoryException eex)
                {
                    ModelState.AddModelError(string.Empty, eex.Message);
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            return Redirect("List");
        }

    }
}
