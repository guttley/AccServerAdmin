using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Commands;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Tools
{
    public class ImportEntryListModel : PageModel
    {
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly IImportEntryListCommand _importEntryListCommand;
        private readonly ILogger<ImportEntryListModel> _logger;

        public ImportEntryListModel(
            IGetServerByIdQuery getServerByIdQuery,
            IImportEntryListCommand importEntryListCommand,
            ILogger<ImportEntryListModel> logger)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _importEntryListCommand = importEntryListCommand;
            _logger = logger;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.ExecuteAsync(id);
        }

        public async Task OnPostImportEntryListAsync(Guid id)
        {
            try
            {
                await _importEntryListCommand.ExecuteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught attempting to import users");
            }
        }

    }
}
