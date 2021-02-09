using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Commands;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class SessionsModel : PageModel
    {
        private readonly IServerSessionQuery _serverSessionQuery;
        private readonly IDeleteResultSessionCommand _deleteSessionCommand;

        [BindProperty]
        public List<Session> Sessions { get; set; }


        public SessionsModel(
            IServerSessionQuery serverSessionQuery,
            IDeleteResultSessionCommand deleteSessionCommand)
        {
            _serverSessionQuery = serverSessionQuery;
            _deleteSessionCommand = deleteSessionCommand;
        }

        private async Task BuildSessionList()
        {
            var sessions = await _serverSessionQuery.Execute();
            Sessions = sessions.OrderByDescending(s => s.SessionTimestamp).ToList();
        }

        public async Task OnGetAsync()
        {
            await BuildSessionList();
        }

        public async Task<IActionResult> OnGetDeleteSession(Guid sessionId)
        {
            await _deleteSessionCommand.Execute(sessionId);

            await BuildSessionList();
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteOldSession(int daysOld)
        {
            var sessions = await _serverSessionQuery.Execute();
            var oldSessions = sessions.Where(s => s.SessionTimestamp < DateTime.Now.Subtract(TimeSpan.FromDays(daysOld)));

            foreach (var session in oldSessions)
            {
                await _deleteSessionCommand.Execute(session.Id);
            }

            return Page();
        }
    }
}