using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
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
        public Guid ServerId { get; set; }

        [BindProperty]
        public Guid EntryListId { get; set; }

        public void OnGet(Guid serverId, Guid entryListId)
        {
            Entry = new Entry
            {
                OverrideDriverInfo = true
            };

            ServerId = serverId;
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
                return Redirect($"EditEntry?serverId={ServerId}&EntryListId={EntryListId}&Id={Entry.Id}");
            }
        }

    }
}
