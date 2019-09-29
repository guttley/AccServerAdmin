using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Dashboard.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IGetServerListQuery _getServerListQuery;
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;

        public DashboardModel(
            IGetAppSettingsQuery getAppSettingsQuery,
            IGetServerListQuery getServerListQuery)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _getServerListQuery = getServerListQuery;
        }

        [BindProperty]
        public bool NeedsConfiguring { get; private set; }

        [BindProperty]
        public IEnumerable<Server> Servers { get; private set; }

        public async Task OnGetAsync()
        {
            var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
            NeedsConfiguring = (settings is null);

            Servers = await _getServerListQuery.ExecuteAsync().ConfigureAwait(false);
        }
    }
}
