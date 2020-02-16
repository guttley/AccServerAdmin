using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Sessions.Commands;
using AccServerAdmin.Application.Sessions.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class DeleteSessionModel : PageModel
    {
        private readonly IGetSessionByIdQuery _getSessionByIdQuery;
        private readonly IDeleteSessionCommand _deleteSessionCommand;
        

        public DeleteSessionModel(
            IGetSessionByIdQuery getServerByIdQuery,
            IDeleteSessionCommand deleteSessionCommand)
        {
            _getSessionByIdQuery = getServerByIdQuery;
            _deleteSessionCommand = deleteSessionCommand;
        }

        [BindProperty]
        public SessionConfiguration Session { get; set; }

        [BindProperty]
        public Guid ServerId { get; set; }

        public async Task OnGetAsync(Guid id, Guid serverId)
        {
            ServerId = serverId;
            Session = await _getSessionByIdQuery.Execute(id).ConfigureAwait(false);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteSessionCommand.Execute(Session.Id).ConfigureAwait(false);
            return RedirectToPage("./Edit", new { Id = ServerId });
        }

    }
}

