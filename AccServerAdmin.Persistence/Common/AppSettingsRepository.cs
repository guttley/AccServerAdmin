using System;
using System.IO;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Persistence.Common
{
    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class AppSettingsRepository : IAppSettingsRepository
    {
        private readonly IDirectory _directory;
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;
        private readonly string _path;
        private readonly string _fullPath;

        public AppSettingsRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter)
        {
            _directory = directory;
            _file = file;
            _jsonConverter = converter;

            var programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            _path = Path.Combine(programData, "AccServerAdmin");
            _fullPath = Path.Combine(_path, "AppSettings.json");
        }

        /// <inheritdoc />
        public void Save(AppSettings config)
        {
            var json = _jsonConverter.SerializeObject(config);
            
            if (!_directory.Exists(_path))
                _directory.CreateDirectory(_path);

            _file.WriteAllText(_fullPath, json);
        }

        /// <inheritdoc />
        public AppSettings Read(bool createIfNotExists)
        {
            if (!_file.Exists(_path))
            {
                if (createIfNotExists)
                {
                    return new AppSettings
                    {
                        ServerBasePath = Path.Combine(_path, "ServerBase"),
                        InstanceBasePath = Path.Combine(_path, "ServerBase")
                    };
                }

                return null;
            }

            var json = _file.ReadAllText(_path);
            var config = _jsonConverter.DeserializeObject<AppSettings>(json);

            return config;
        }
    }
}
