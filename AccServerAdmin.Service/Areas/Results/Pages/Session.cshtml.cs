using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class SessionModel : PageModel
    {
        private readonly ISessionResultQuery _sessionResultQuery;

        [BindProperty]
        public Session Session { get; set; }


        public SessionModel(ISessionResultQuery sessionResultQuery)
        {
            _sessionResultQuery = sessionResultQuery;
        }

        public async Task OnGetAsync(Guid sessionId)
        {
            Session = await _sessionResultQuery.Execute(sessionId).ConfigureAwait(false);
        }

    }
}