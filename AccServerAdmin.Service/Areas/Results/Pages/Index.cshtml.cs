using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IServerSessionQuery _serverSessionQuery;

        [BindProperty]
        public List<Session> Sessions { get; set; }


        public IndexModel(
            IServerSessionQuery serverSessionQuery)
        {
            _serverSessionQuery = serverSessionQuery;
        }

        public async Task OnGetAsync()
        {
            var sessions = await _serverSessionQuery.Execute();
            Sessions = sessions.OrderByDescending(s => s.SessionTimestamp).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

    }
}