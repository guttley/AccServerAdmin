using System.Collections.Generic;
using System.Linq;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IGetServerListQuery _getServerListQuery;
        private readonly IAppSettingsRepository _appSettingsRepository;

        public DashboardModel(
            IAppSettingsRepository appSettingsRepository,
            IGetServerListQuery getServerListQuery)
        {
            _appSettingsRepository = appSettingsRepository;
            _getServerListQuery = getServerListQuery;
        }

        [BindProperty]
        public bool NeedsConfiguring { get; private set; }

        [BindProperty]
        public IList<Server> Servers { get; private set; }

        public void OnGet()
        {
            var settings = _appSettingsRepository.Read(false);
            NeedsConfiguring = settings is null;

            Servers = _getServerListQuery.Execute().ToList();
        }
    }
}
