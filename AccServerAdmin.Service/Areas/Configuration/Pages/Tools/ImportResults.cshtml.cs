using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Tools
{
    public class ImportResultsModel : PageModel
    {
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly IResultImporter _resultImporter;
        private readonly ILogger<ImportResultsModel> _logger;

        public ImportResultsModel(
            IGetServerByIdQuery getServerByIdQuery,
            IResultImporter resultImporter,
            ILogger<ImportResultsModel> logger)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _resultImporter = resultImporter;
            _logger = logger;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.Execute(id);
        }

        public async Task OnPostImportResultsAsync(Guid id, string serverName)
        {
            try
            {
                await _resultImporter.Execute(id, serverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught attempting to import users");
            }
        }

    }
}
