using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class CopyServerModel : PageModel
    {
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly ICopyServerCommand _copyServerCommand;

        public CopyServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            ICopyServerCommand copyServerCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _copyServerCommand = copyServerCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.Execute(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _copyServerCommand.Execute(Server.Id);
            return RedirectToPage("./List");
        }

    }
}

