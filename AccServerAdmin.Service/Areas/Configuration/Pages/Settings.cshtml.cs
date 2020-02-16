using System;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.AppSettings;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly IGetAppSettingsQuery _getAppSettingsQuery;
        private readonly ISaveAppSettingsCommand _saveAppSettingsCommand;
        private readonly IUnitOfWork _unitOfWork;
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
            IUnitOfWork unitOfWork,
            IDirectory directory)
        {
            _getAppSettingsQuery = getAppSettingsQuery;
            _saveAppSettingsCommand = saveAppSettingsCommand;
            _unitOfWork = unitOfWork;
            _directory = directory;
        }

        public async Task OnGetAsync()
        {
            Settings = await _getAppSettingsQuery.Execute();

            if (Settings is null)
            {
                Settings = new AppSettings
                {
                    ServerBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AccServerAdmin", "ServerBase"),
                    InstanceBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AccServerAdmin", "ServerInstances"),
                    AdminPassphrase = "ChangeThisPassphrase"
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

                await _saveAppSettingsCommand.Execute(Settings);
                await _unitOfWork.SaveChanges();
                _directory.CreateDirectory(Settings.ServerBasePath);
                _directory.CreateDirectory(Settings.InstanceBasePath);
                Globals.NeedsConfiguring = false;

                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}