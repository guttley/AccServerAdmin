using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class DeleteEntryModel : PageModel
    {
        private readonly IGetEntryByIdQuery _getEntryByIdQuery;
        private readonly IDeleteEntryCommand _deleteEntryCommand;
        
        public DeleteEntryModel(
            IGetEntryByIdQuery getEntryByIdQuery,
            IDeleteEntryCommand deleteEntryCommand)
        {
            _getEntryByIdQuery = getEntryByIdQuery;
            _deleteEntryCommand = deleteEntryCommand;
        }

        [BindProperty]
        public Entry Entry { get; set; }

        [BindProperty]
        public Guid ServerId { get; set; }

        public async Task OnGetAsync(Guid id, Guid serverId)
        {
            ServerId = serverId;
            Entry = await _getEntryByIdQuery.Execute(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteEntryCommand.Execute(Entry.Id);
            return RedirectToPage("./Edit", new { Id = ServerId });
        }

    }
}

