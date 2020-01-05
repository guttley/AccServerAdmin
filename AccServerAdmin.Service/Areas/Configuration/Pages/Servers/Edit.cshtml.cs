using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class EditServerModel : PageModel
    {
        private readonly IUpdateServerCommand _updateServerCommand;
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly IGetDuplicatePortQuery _getDuplicatePortQuery;

        public EditServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            IGetDuplicatePortQuery getDuplicatePortQuery,
            IUpdateServerCommand updateServerCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _getDuplicatePortQuery = getDuplicatePortQuery;
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
                {"spa", "Spa-Francorchamps" },
                {"nurburgring", "Nurburgring" },
                {"barcelona", "Barcelona" },
                {"hungaroring", "Hungaroring" },
                {"zandvoort", "Zandvoort" },
                {"monza_2019", "Monza 2019" },
                {"zolder_2019", "Zolder 2019" },
                {"brands_hatch_2019", "Brands Hatch 2019" },
                {"silverstone_2019", "Silverstone 2019" },
                {"paul_ricard_2019", "Paul Ricard 2019" },
                {"misano_2019", "Misano 2019" },
                {"spa_2019", "Spa-Francorchampss 2019" },
                {"nurburgring_2019", "Nurburgring 2019" },
                {"barcelona_2019", "Barcelona 2019" },
                {"hungaroring_2019", "Hungaroring 2019" },
                {"zandvoort_2019", "Zandvoort 2019" },
            };

            var eventTypes = new Dictionary<string, string>
            {
                {"E_3h", "Endurance 3 Hour"},
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

        private async Task ValidateNetworkCfgAsync()
        {
            if (Server.NetworkCfg.TcpPort < 1024 || Server.NetworkCfg.TcpPort > ushort.MaxValue)
                ModelState.AddModelError(nameof(NetworkCfg.TcpPort), $"TcpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.UdpPort < 1024 || Server.NetworkCfg.UdpPort > ushort.MaxValue)
                ModelState.AddModelError(nameof(NetworkCfg.UdpPort), $"UdpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.MaxConnections == 0 || Server.NetworkCfg.MaxConnections > 64)
                ModelState.AddModelError(nameof(NetworkCfg.MaxConnections), "MaxConnections must be between 1 and 64");


            if (await _getDuplicatePortQuery.ExecuteAsync(Server.Id, Server.NetworkCfg.TcpPort, Server.NetworkCfg.UdpPort).ConfigureAwait(false))
            {
                ModelState.AddModelError(nameof(NetworkCfg.TcpPort), "Tcp or Udp Port is in use by another server");
            }

        }

        private void ValidateGameCfg()
        {
            if (Server.GameCfg.Password is null)
            {
                Server.GameCfg.Password = string.Empty;
            }

            if (Server.GameCfg.SpectatorPassword is null)
            {
                Server.GameCfg.SpectatorPassword = string.Empty;
            }

            if (string.IsNullOrEmpty(Server.GameCfg.AdminPassword))
                ModelState.AddModelError(nameof(GameCfg.AdminPassword), "You must supply an Admin password!!");

            if (!string.IsNullOrEmpty(Server.GameCfg.Password))
            {
                if (Server.GameCfg.Password == Server.GameCfg.AdminPassword)
                    ModelState.AddModelError(nameof(GameCfg.Password), "You must supply a different password for the server and admin passwords");

                if (Server.GameCfg.Password == Server.GameCfg.SpectatorPassword)
                    ModelState.AddModelError(nameof(GameCfg.SpectatorPassword), "You must supply a different password for the spectator password");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await ValidateNetworkCfgAsync().ConfigureAwait(false);
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

