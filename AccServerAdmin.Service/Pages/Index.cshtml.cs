using AccServerAdmin.Application;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccServerAdmin.Service.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGetServerListQuery _getServerListQuery;
        private readonly IProcessManager _processManager;
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;

        public IndexModel(
            IProcessManager processManager,
            IGetAppSettingsQuery getAppSettingsQuery,
            IGetServerListQuery getServerListQuery)
        {
            _processManager = processManager;
            _getAppSettingsQuery = getAppSettingsQuery;
            _getServerListQuery = getServerListQuery;
        }

        [BindProperty]
        public IEnumerable<DashItem> DashItems { get; private set; }

        public async Task OnGetAsync()
        {
            var settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);
            var servers = await _getServerListQuery.ExecuteAsync().ConfigureAwait(false);

            DashItems = servers.Select(s => new DashItem
            {
                Server = s,
                ProcessInfo = _processManager.ServerProcesses.FirstOrDefault(p => p.ServerId == s.Id)
            }).ToList();


            Globals.NeedsConfiguring = settings is null;
        }

        public async Task<ActionResult> OnPostStartServerAsync(Guid serverId)
        {
            await _processManager.StartServerAsync(serverId);
            return new RedirectResult("Index");
        }

        public ActionResult OnPostStopServer(Guid serverId)
        {
            _processManager.StopServer(serverId);
            return new RedirectResult("Index");
        }
    }
}
