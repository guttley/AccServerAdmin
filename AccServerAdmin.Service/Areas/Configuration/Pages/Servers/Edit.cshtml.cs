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

        public EditServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            IUpdateServerCommand updateServerCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _updateServerCommand = updateServerCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public SelectList Tracks { get; set; }

        public SelectList EventTypes { get; set; }

        public SelectList TrackMedals { get; set; }

        public SelectList SafetyRatings { get; set; }

        public SelectList RacecraftRatings { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.ExecuteAsync(id).ConfigureAwait(false);
            BuildBindingLists();
        }

        private void BuildBindingLists()
        {
            var tracks = new Dictionary<string, string>
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

            var eventTypes = new Dictionary<string, string>
            {
                { "E_3h", "Endurance 3 Hour"},
                {"E_6h", "Endurance 6 Hour"},
                {"E_24h", "Endurance 24 Hour"},
                {"Sprint", "Sprint"}
            };

            var trackMedals = new Dictionary<int, string>
            {
                { 0, "0"},
                { 1, "1"},
                { 2, "2"},
                { 3, "3"},
            };

            var ratings = new Dictionary<int, string>();

            for (int i = -1; i < 100; i++)
            {
                ratings.Add(i, i.ToString());
            }

            Tracks = new SelectList(tracks, "Key", "Value", Server.EventCfg.Track);
            EventTypes = new SelectList(eventTypes, "Key", "Value", Server.EventCfg.EventType);
            TrackMedals = new SelectList(trackMedals, "Key", "Value", Server.GameCfg.TrackMedalsRequirement);
            SafetyRatings = new SelectList(ratings, "Key", "Value", Server.GameCfg.SafetyRatingRequirement);
            RacecraftRatings = new SelectList(ratings, "Key", "Value", Server.GameCfg.RacecraftRatingRequirement);
        }

        private void ValidateNetworkCfg()
        {
            if (Server.NetworkCfg.TcpPort < 1024 || Server.NetworkCfg.TcpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkCfg.TcpPort", $"TcpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.UdpPort < 1024 || Server.NetworkCfg.UdpPort > ushort.MaxValue)
                ModelState.AddModelError("NetworkCfg.UdpPort", $"UdpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.MaxClients == 0 || Server.NetworkCfg.MaxClients > 32)
                ModelState.AddModelError("NetworkCfg.MaxClients", "MaxClients must be between 1 and 32");
        }

        private void ValidateGameCfg()
        {
            if (string.IsNullOrEmpty(Server.GameCfg.AdminPassword))
                ModelState.AddModelError("GameCfg.AdminPassword", "You must supply an Admin password!!");

            if (!string.IsNullOrEmpty(Server.GameCfg.Password))
            {
                if (Server.GameCfg.Password == Server.GameCfg.AdminPassword)
                    ModelState.AddModelError("GameCfg.Password", "You must supply a different password for the Admin password");

                if (Server.GameCfg.Password == Server.GameCfg.SpectatorPassword)
                    ModelState.AddModelError("GameCfg.Password", "You must supply a different password for the spectator password");
            }

            if (Server.GameCfg.SpectatorSlots > 32)
                ModelState.AddModelError("GameCfg.SpectatorSlots", "Spectator Slots must be less than 32");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ValidateNetworkCfg();
                ValidateGameCfg();
            }

            if (!ModelState.IsValid)
            {
                BuildBindingLists();
                return Page();
            }

            await _updateServerCommand.ExecuteAsync(Server).ConfigureAwait(false);
            return RedirectToPage("./List");
        }
    }
}

