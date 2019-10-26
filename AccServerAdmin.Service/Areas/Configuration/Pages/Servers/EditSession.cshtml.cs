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
        private readonly IUpdateSessionCommand _updateSessionCommand;
        private readonly Dictionary<string, string> _sessionTypes;
        private readonly Dictionary<int, string> _sessionDays;

        public EditSessionModel(
            IGetSessionByIdQuery getSessionByIdQuery,
            IUpdateSessionCommand updateSessionCommand)
        {
            _getSessionByIdQuery = getSessionByIdQuery;
            _updateSessionCommand = updateSessionCommand;

            _sessionTypes = new Dictionary<string, string>
            {
                { "P", "Practice" },
                { "Q", "Qualification" },
                { "R", "Race" },
            };

            _sessionDays = new Dictionary<int, string>
            {
                { 1, "Friday" },
                { 2, "Saturday" },
                { 3, "Sunday" },
            };
        }

        [BindProperty]
        public Guid ServerId { get; set; }

        [BindProperty]
        public SessionConfiguration Session { get; set; }

        [BindProperty]
        public SelectList SessionTypes { get; set; }

        [BindProperty]
        public SelectList SessionDays { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Session = await _getSessionByIdQuery.ExecuteAsync(id).ConfigureAwait(false);
            BuildBindingLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ValidateSessionConfiguration();

            }

            if (!ModelState.IsValid)
            {
                BuildBindingLists();
                return Page();
            }

            await _updateSessionCommand.ExecuteAsync(ServerId, Session).ConfigureAwait(false);
            return RedirectToPage("./Edit");
        }

        private void BuildBindingLists()
        {
            SessionTypes = new SelectList(_sessionTypes, "Key", "Value", null);
            SessionDays = new SelectList(_sessionDays, "Key", "Value", null);
        }

        private void ValidateSessionConfiguration()
        {
            /*
            if (Server.NetworkConfiguration.TcpPort < 1024 || Server.NetworkConfiguration.TcpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkConfiguration.TcpPort", $"TcpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkConfiguration.UdpPort < 1024 || Server.NetworkConfiguration.UdpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkConfiguration.UdpPort", $"UdpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkConfiguration.MaxClients == 0 || Server.NetworkConfiguration.MaxClients > 32)
                ModelState.AddModelError("NetworkConfiguration.MaxClients", "MaxClients must be between 1 and 32");
                */
        }

    }
}

