using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class EditServerModel : PageModel
    {
        private readonly IUpdateServerCommand _updateServerCommand;
        private readonly IGetServerByIdQuery _getServerByIdQuery;

        private readonly Dictionary<string, string> _tracks;
        private readonly Dictionary<string, string> _eventTypes;
        private readonly Dictionary<int, string> _trackMedals;
        private readonly Dictionary<int, string> _ratings;
        private readonly Dictionary<string, string> _sessionTypes;
        private readonly Dictionary<int, string> _sessionDays;

        public EditServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            IUpdateServerCommand updateServerCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _updateServerCommand = updateServerCommand;

            _tracks = new Dictionary<string, string>
            {
                {"monza", "Monza" },
                {"zolder", "Zolder" },
                {"brands_hatch", "Brands Hatch" },
                {"silverstone", "Silverstone" },
                {"paul_ricard", "Paul Ricard" },
                {"misano", "Misano" },
                {"spa", "Spa" },
                {"nurburgring", "Nurburgring" },
                {"barcelona", "Barcelona" },
                {"hungaroring", "Hungaroring" },
            };

            _eventTypes = new Dictionary<string, string>
            {
                { "E_3h", "Endurance 3 Hour"},
                {"E_6h", "Endurance 6 Hour"},
                {"E_24h", "Endurance 24 Hour"},
                {"Sprint", "Sprint"}
            };

            _trackMedals = new Dictionary<int, string>
            {
                { 0, "0"},
                { 1, "1"},
                { 2, "2"},
                { 3, "3"},
            };

            _ratings = new Dictionary<int, string>();

            for (int i = -1; i < 100; i++)
            {
                _ratings.Add(i, i.ToString());
            }

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
        public Server Server { get; set; }

        public SelectList Tracks { get; set; }

        public SelectList EventTypes { get; set; }

        public SelectList TrackMedals { get; set; }

        public SelectList SafetyRatings { get; set; }

        public SelectList RacecraftRatings { get; set; }

        public SelectList SessionTypes { get; set; }

        public SelectList SessionDays { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.ExecuteAsync(id).ConfigureAwait(false);
            BuildBindingLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ValidateNetworkConfiguration();
                ValidateGameConfiguration();
            }

            if (!ModelState.IsValid)
            {
                BuildBindingLists();
                return Page();
            }

            await _updateServerCommand.ExecuteAsync(Server).ConfigureAwait(false);
            return RedirectToPage("./List");
        }

        private void BuildBindingLists()
        {
            Tracks = new SelectList(_tracks, "Key", "Value", Server.EventConfiguration.Track);
            EventTypes = new SelectList(_eventTypes, "Key", "Value", Server.EventConfiguration.EventType);
            TrackMedals = new SelectList(_trackMedals, "Key", "Value", Server.GameConfiguration.TrackMedalsRequirement);
            SafetyRatings = new SelectList(_ratings, "Key", "Value", Server.GameConfiguration.SafetyRatingRequirement);
            RacecraftRatings = new SelectList(_ratings, "Key", "Value", Server.GameConfiguration.RacecraftRatingRequirement);
            SessionTypes = new SelectList(_sessionTypes, "Key", "Value", null);
            SessionDays = new SelectList(_sessionDays, "Key", "Value", null);
        }

        private void ValidateNetworkConfiguration()
        {
            if (Server.NetworkConfiguration.TcpPort < 1024 || Server.NetworkConfiguration.TcpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkConfiguration.TcpPort", $"TcpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkConfiguration.UdpPort < 1024 || Server.NetworkConfiguration.UdpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkConfiguration.UdpPort", $"UdpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkConfiguration.MaxClients == 0 || Server.NetworkConfiguration.MaxClients > 32)
                ModelState.AddModelError("NetworkConfiguration.MaxClients", "MaxClients must be between 1 and 32");
        }

        private void ValidateGameConfiguration()
        {
            if (string.IsNullOrEmpty(Server.GameConfiguration.AdminPassword))
                ModelState.AddModelError("GameConfiguration.AdminPassword", "You must supply an Admin password!!");

            if (!string.IsNullOrEmpty(Server.GameConfiguration.Password))
            {
                if (Server.GameConfiguration.Password == Server.GameConfiguration.AdminPassword)
                    ModelState.AddModelError("GameConfiguration.Password", "You must supply a different password for the Admin password");

                if (Server.GameConfiguration.Password == Server.GameConfiguration.SpectatorPassword)
                    ModelState.AddModelError("GameConfiguration.Password", "You must supply a different password for the spectator password");
            }

            if (Server.GameConfiguration.SpectatorSlots > 32)
                ModelState.AddModelError("GameConfiguration.SpectatorSlots", "Spectator Slots must be less than 32");
        }
    }
}

