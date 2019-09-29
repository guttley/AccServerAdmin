using System;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly ISaveAppSettingsCommand _saveAppSettingsCommand;
        private readonly IDirectory _directory;

        [BindProperty]
        public AppSettings Settings { get; set; }

        [BindProperty]
        public bool CreateDirectories { get; set; }

        [BindProperty]
        public bool IsDefaulted { get; private set; }

        public SettingsModel(
            IGetAppSettingsQuery getAppSettingsQuery,
            ISaveAppSettingsCommand saveAppSettingsCommand,
            IDirectory directory)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _saveAppSettingsCommand = saveAppSettingsCommand;
            _directory = directory;
        }

        public async Task OnGetAsync()
        {
            Settings = await _getAppSettingsQuery.ExecuteAsync().ConfigureAwait(false);

            if (Settings is null)
            {
                Settings = new AppSettings
                {
                    ServerBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AccServerAdmin", "ServerBase"),
                    InstanceBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AccServerAdmin", "ServerInstances")
                };

                IsDefaulted = true;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!CreateDirectories)
                {
                    if (!_directory.Exists(Settings.ServerBasePath))
                    {
                        ModelState.AddModelError("Settings.ServerBasePath", "Directory does not exist");
                    }

                    if (!_directory.Exists(Settings.InstanceBasePath))
                    {
                        ModelState.AddModelError("Settings.InstanceBasePath", "Directory does not exist");
                    }

                    if (!ModelState.IsValid)
                        return Page();
                }

                await _saveAppSettingsCommand.ExecuteAsync(Settings);
                _directory.CreateDirectory(Settings.ServerBasePath);
                _directory.CreateDirectory(Settings.InstanceBasePath);
            }

            return Page();            
        }

    }
}