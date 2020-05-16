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
using AccServerAdmin.Application.Results.Queries;

namespace AccServerAdmin.Service.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGetServerListQuery _getServerListQuery;
        private readonly IProcessManager _processManager;
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly IGetImportableResultsQuery _getImportableResultsQuery; 

        public IndexModel(
            IProcessManager processManager,
            IGetAppSettingsQuery getAppSettingsQuery,
            IGetServerListQuery getServerListQuery,
            IGetImportableResultsQuery getImportableResultsQuery)
        {
            _processManager = processManager;
            _getAppSettingsQuery = getAppSettingsQuery;
            _getServerListQuery = getServerListQuery;
            _getImportableResultsQuery = getImportableResultsQuery;
        }

        [BindProperty]
        public IEnumerable<DashItem> DashItems { get; private set; }

        public async Task OnGetAsync()
        {
            var settings = await _getAppSettingsQuery.Execute();
            var servers = await _getServerListQuery.Execute();
            
            var items = servers.Select(async s => new DashItem
            {
                Server = s,
                ProcessInfo = _processManager.ServerProcesses.FirstOrDefault(p => p.ServerId == s.Id),
                HasImportableResults = s.CollectResults && await _getImportableResultsQuery.Execute(s.Id)
            }).ToList();

            DashItems = await Task.WhenAll(items);
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
