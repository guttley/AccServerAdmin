using System;
using System.IO;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Persistence.Common
{
    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class BaseConfigRepository<T> : IConfigRepository<T>  where T : new()
    {
        private readonly IDirectory _directory;
        private readonly IFile _file;
        private readonly IJsonConverter _jsonConverter;
        private readonly string _configDir;
        private readonly string _filename;

        public BaseConfigRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter,
            string configDir,
            string filename)
        {
            _directory = directory;
            _file = file;
            _jsonConverter = converter;
            _configDir = configDir;
            _filename = filename;
        }

        /// <inheritdoc />
        public virtual  T New()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Save(string serverDirectory,  T config)
        {
            var path = Path.Combine(serverDirectory, _configDir);
            var json = _jsonConverter.SerializeObject(config);

            if (!_directory.Exists(path))
                _directory.CreateDirectory(path);

            path = Path.Combine(path, _filename);
            _file.WriteAllText(path, json);
        }

        /// <inheritdoc />
        public T Read(string serverDirectory)
        {
            var path = Path.Combine(serverDirectory, _configDir, _filename);

            if (!_file.Exists(path))
                return New();

            var json = _file.ReadAllText(path);
            var config = _jsonConverter.DeserializeObject<T>(json);

            return config;

        }
    }
}
