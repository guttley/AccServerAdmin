using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class LapsModel : PageModel
    {
        private readonly ISessionLapsQuery _sessionLapsQuery;

        [BindProperty]
        public Session Session { get; set; }

        public LapsModel(ISessionLapsQuery sessionLapsQuery)
        {
            _sessionLapsQuery = sessionLapsQuery;
        }

        public async Task OnGetAsync(Guid sessionId, Guid carId)
        {
            Session = await _sessionLapsQuery.Execute(sessionId, carId).ConfigureAwait(false);
        }

    }
}