using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.EntryLists
{
    public class AddEntryModel : PageModel
    {
        private readonly ICreateEntryCommand _createEntryCommand;

        public AddEntryModel(ICreateEntryCommand createEntryCommand)
        {
            _createEntryCommand = createEntryCommand;
        }

        [BindProperty]
        public Entry Entry { get; set; }

        [BindProperty]
        public Guid GlobalEntryListId { get; set; }

        [BindProperty]
        public Guid EntryListId { get; set; }

        public void OnGet(Guid globalEntryListId, Guid entryListId)
        {
            GlobalEntryListId = globalEntryListId;
            EntryListId = entryListId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Entry.EntryListId = EntryListId;
                    await _createEntryCommand.Execute(Entry);
                }
                catch (RaceNumberNotUniqueException rex)
                {
                    ModelState.AddModelError("Entry.RaceNumber", rex.Message);
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                return Redirect($"EditEntry?globalEntryListId={GlobalEntryListId}&EntryListId={EntryListId}&Id={Entry.Id}");
            }
        }

    }
}
