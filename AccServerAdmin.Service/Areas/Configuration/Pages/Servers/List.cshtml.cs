using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class ServerListModel : PageModel
    {
        private readonly IGetServerListQuery _getServerListQuery;

        public ServerListModel(IGetServerListQuery getServerListQuery)
        {
            _getServerListQuery = getServerListQuery;
        }

        [BindProperty]
        public IEnumerable<Server> Servers { get; set; }

        public async Task OnGetAsync()
        {
            Servers = await _getServerListQuery.Execute().ConfigureAwait(false);
        }
    }
}
