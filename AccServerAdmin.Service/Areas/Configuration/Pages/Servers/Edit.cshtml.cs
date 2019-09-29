using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class EditServerModel : PageModel
    {
        private readonly IUpdateServerCommand _updateServerCommand;
        private readonly IGetServerByIdQuery _getServerByIdQuery;

        public EditServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            IUpdateServerCommand updateServerCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _updateServerCommand = updateServerCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.ExecuteAsync(id).ConfigureAwait(false);
        }
    }
}
