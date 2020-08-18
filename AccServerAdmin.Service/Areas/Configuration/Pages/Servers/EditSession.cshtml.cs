using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Sessions.Commands;
using AccServerAdmin.Application.Sessions.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class EditSessionModel : PageModel
    {
        private readonly IGetSessionByIdQuery _getSessionByIdQuery;
        private readonly ICreateSessionCommand _createSessionCommand;
        private readonly IUpdateSessionCommand _updateSessionCommand;

        public EditSessionModel(
            IGetSessionByIdQuery getSessionByIdQuery,
            ICreateSessionCommand createSessionCommand,
            IUpdateSessionCommand updateSessionCommand)
        {
            _getSessionByIdQuery = getSessionByIdQuery;
            _createSessionCommand = createSessionCommand;
            _updateSessionCommand = updateSessionCommand;

            BuildBindingLists();
        }

        [BindProperty]
        public Guid ServerId { get; set; }

        [BindProperty]
        public SessionConfiguration Session { get; set; }

        public SelectList SessionTypes { get; set; }

        public SelectList SessionDays { get; set; }

        public async Task OnGetAsync(Guid serverId, Guid eventId, Guid id)
        {
            ServerId = serverId;
            
            if (id == Guid.Empty)
            {
                Session = new SessionConfiguration
                {
                    EventCfgId = eventId,
                    SessionType = SessionType.Practice,
                    DayOfWeekend = 1,
                    HourOfDay = 10,
                    TimeMultiplier = 1,
                    SessionDurationMinutes = 20
                };
            }
            else
            {
                Session = await _getSessionByIdQuery.Execute(id);
            }                       
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ValidateSessionConfiguration();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Session.Id == Guid.Empty)
            {
                await _createSessionCommand.Execute(ServerId, Session);                
            }
            else
            {
                await _updateSessionCommand.Execute(ServerId, Session);
            }

            return Redirect($"/Configuration/Servers/Edit?Id={ServerId}#nav-tab-sessions");
        }

        private void BuildBindingLists()
        {
            SessionTypes = new SelectList(ListData.SessionTypes, "Key", "Value", null);
            SessionDays = new SelectList(ListData.SessionDays, "Key", "Value", null);
        }

        private void ValidateSessionConfiguration()
        {

        }

    }
}

