using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Tools
{
    public class ImportEntryListModel : PageModel
    {
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly IImportEntryListCommand _importEntryListCommand;

        public ImportEntryListModel(
            IGetServerByIdQuery getServerByIdQuery,
            IImportEntryListCommand importEntryListCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _importEntryListCommand = importEntryListCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.ExecuteAsync(id);
        }

        public Task OnPostImportEntryListAsync(Guid id)
        {
            Task.Run(() => _importEntryListCommand.ExecuteAsync(id));
            return Task.CompletedTask;
        }

    }
}
