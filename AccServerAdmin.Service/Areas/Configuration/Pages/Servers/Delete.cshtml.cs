using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class DeleteServerModel : PageModel
    {
        private readonly IDeleteServerCommand _deleteServerCommand;
        private readonly IGetServerByIdQuery _getServerByIdQuery;

        public DeleteServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            IDeleteServerCommand deleteServerCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _deleteServerCommand = deleteServerCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.Execute(id).ConfigureAwait(false);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteServerCommand.Execute(Server.Id).ConfigureAwait(false);
            return RedirectToPage("./List");
        }

    }
}

