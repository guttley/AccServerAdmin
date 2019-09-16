using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;
using System.IO;

namespace AccServerAdmin.Service.Helpers
{
    public class ConfigValiator
    {
        private readonly AppSettings _settings;

        public ConfigValiator(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        /// <summary>
        /// Validate the base server path and the existance of known files
        /// </summary>
        public void Execute()
        {
            if (!Directory.Exists(_settings.ServerBasePath))
            {
                throw new DirectoryNotFoundException($"Cannot find configured path to the base ACC server: {_settings.ServerBasePath}");
            }

            if (!Directory.Exists(Path.Combine(_settings.ServerBasePath, "accServer.exe")))
            {
                throw new DirectoryNotFoundException($"Cannot find accServer.exe in configured path to the base ACC server: {_settings.ServerBasePath}");
            }

            if (!Directory.Exists(Path.Combine(_settings.ServerBasePath, "accServer.pdb")))
            {
                throw new DirectoryNotFoundException($"Cannot find accServer.pdb in configured path to the base ACC server: {_settings.ServerBasePath}");
            }
        }

    }
}
