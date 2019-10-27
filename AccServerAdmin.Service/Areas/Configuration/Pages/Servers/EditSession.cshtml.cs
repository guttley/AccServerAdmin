using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Sessions.Commands;
using AccServerAdmin.Application.Sessions.Queries;
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
                Session = await _getSessionByIdQuery.ExecuteAsync(id).ConfigureAwait(false);
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
                await _createSessionCommand.ExecuteAsync(ServerId, Session).ConfigureAwait(false);                
            }
            else
            {
                await _updateSessionCommand.ExecuteAsync(ServerId, Session).ConfigureAwait(false);
            }

            return RedirectToPage("./Edit", new { Id = ServerId });
        }

        private void BuildBindingLists()
        {
            var sessionTypes = new Dictionary<string, string>
            {
                { "P", "Practice" },
                { "Q", "Qualification" },
                { "R", "Race" },
            };

            var sessionDays = new Dictionary<int, string>
            {
                { 1, "Friday" },
                { 2, "Saturday" },
                { 3, "Sunday" },
            };

            SessionTypes = new SelectList(sessionTypes, "Key", "Value", null);
            SessionDays = new SelectList(sessionDays, "Key", "Value", null);
        }

        private void ValidateSessionConfiguration()
        {
            /*
            if (Server.NetworkCfg.TcpPort < 1024 || Server.NetworkCfg.TcpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkCfg.TcpPort", $"TcpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.UdpPort < 1024 || Server.NetworkCfg.UdpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkCfg.UdpPort", $"UdpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.MaxClients == 0 || Server.NetworkCfg.MaxClients > 32)
                ModelState.AddModelError("NetworkCfg.MaxClients", "MaxClients must be between 1 and 32");
                */
        }

    }
}

