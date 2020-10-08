using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Commands;
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
        private readonly IGridResetCommand _resetGridCommand;
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly IGetDuplicatePortQuery _getDuplicatePortQuery;

        public EditServerModel(
            IGetServerByIdQuery getServerByIdQuery,
            IGetDuplicatePortQuery getDuplicatePortQuery,
            IUpdateServerCommand updateServerCommand,
            IGridResetCommand resetGridCommand)
        {
            _getServerByIdQuery = getServerByIdQuery;
            _getDuplicatePortQuery = getDuplicatePortQuery;
            _updateServerCommand = updateServerCommand;
            _resetGridCommand = resetGridCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public SelectList CarGroups { get; set; }

        public SelectList Tracks { get; set; }

        public SelectList EventTypes { get; set; }

        public SelectList FormationLapTypes { get; set; }

        public SelectList TrackMedals { get; set; }

        public SelectList SafetyRatings { get; set; }

        public SelectList RacecraftRatings { get; set; }

        public SelectList StabilityControlSettings { get; set; }

        private void BuildBindingLists()
        {
            CarGroups = new SelectList(ListData.CarGroups, "Key", "Value", Server.GameCfg.CarGroup);
            Tracks = new SelectList(ListData.Tracks, "Key", "Value", Server.EventCfg.Track);
            EventTypes = new SelectList(ListData.EventTypes, "Key", "Value", Server.EventCfg.EventType);
            FormationLapTypes = new SelectList(ListData.FormationLapTypes, "Key", "Value", Server.EventCfg.EventType);
            TrackMedals = new SelectList(ListData.TrackMedals, "Key", "Value", Server.GameCfg.TrackMedalsRequirement);
            SafetyRatings = new SelectList(ListData.Ratings, "Key", "Value", Server.GameCfg.SafetyRatingRequirement);
            RacecraftRatings = new SelectList(ListData.Ratings, "Key", "Value", Server.GameCfg.RacecraftRatingRequirement);
            StabilityControlSettings = new SelectList(ListData.Ratings, "Key", "Value", Server.AssistRules.StabilityControlLevelMax);
        }

        private async Task ValidateNetworkCfgAsync()
        {
            if (Server.NetworkCfg.TcpPort < 1024 || Server.NetworkCfg.TcpPort > ushort.MaxValue)
                ModelState.AddModelError("Server.NetworkCfg.TcpPort", $"TcpPort must be greater than 1024 and less than {ushort.MaxValue}");

            if (Server.NetworkCfg.UdpPort < 1024 || Server.NetworkCfg.UdpPort > ushort.MaxValue)
                ModelState.AddModelError("Server.NetworkCfg.UdpPort", $"UdpPort must be greater than 1024 and less than {ushort.MaxValue}");

//            if (Server.NetworkCfg.MaxConnections == 0 || Server.NetworkCfg.MaxConnections > 64)
//                ModelState.AddModelError("Server.NetworkCfg.MaxConnections", "MaxConnections must be between 1 and 64");

            if (await _getDuplicatePortQuery.Execute(Server.Id, Server.NetworkCfg.TcpPort, Server.NetworkCfg.UdpPort))
            {
                ModelState.AddModelError("Server.NetworkCfg.TcpPort", "Tcp or Udp Port is in use by another server");
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
                ModelState.AddModelError("Server.GameCfg.AdminPassword", "You must supply an Admin password!!");

            if (!string.IsNullOrEmpty(Server.GameCfg.Password))
            {
                if (Server.GameCfg.Password == Server.GameCfg.AdminPassword)
                    ModelState.AddModelError("Server.GameCfg.Password", "You must supply a different password for the server and admin passwords");

                if (Server.GameCfg.Password == Server.GameCfg.SpectatorPassword)
                    ModelState.AddModelError("Server.GameCfg.SpectatorPassword", "You must supply a different password for the spectator password");
            }
        }

        public async Task OnGetAsync(Guid id)
        {
            Server = await _getServerByIdQuery.Execute(id);
            BuildBindingLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await ValidateNetworkCfgAsync();
                ValidateGameCfg();
            }

            if (!ModelState.IsValid)
            {
                BuildBindingLists();
                return Page();
            }

            await _updateServerCommand.Execute(Server);
            return RedirectToPage("./List");
        }

        public async Task OnGetResetGrid(Guid serverId, Guid entryListId)
        {
            await _resetGridCommand.Execute(entryListId);
            await OnGetAsync(serverId);
        }
    }
}

