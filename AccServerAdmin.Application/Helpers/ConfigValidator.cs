using System.IO;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Helpers
{
    public class ConfigValidator
    {
        private readonly IDirectory _directory;
        private readonly IFile _file;
        private readonly AppSettings _settings;

        public ConfigValidator(
            IDirectory directory,
            IFile file,
            IAppSettingsRepository settings)
        {
            _directory = directory;
            _file = file;
            _settings = settings.Read();
        }

        /// <summary>
        /// Validate the base server path and the existence of known files
        /// </summary>
        public void Execute()
        {
            if (!_directory.Exists(_settings.InstanceBasePath))
                _directory.CreateDirectory(_settings.InstanceBasePath);

            if (!_directory.Exists(_settings.ServerBasePath))
            {
                throw new DirectoryNotFoundException($"Cannot find configured path to the base ACC server: {_settings.ServerBasePath}");
            }

            if (!_file.Exists(Path.Combine(_settings.ServerBasePath, "accServer.exe")))
            {
                throw new FileNotFoundException($"Cannot find accServer.exe in configured path to the base ACC server: {_settings.ServerBasePath}");
            }

            if (!_file.Exists(Path.Combine(_settings.ServerBasePath, "accServer.pdb")))
            {
                throw new FileNotFoundException($"Cannot find accServer.pdb in configured path to the base ACC server: {_settings.ServerBasePath}");
            }
        }

    }
}
