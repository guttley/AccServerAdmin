using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Common
{
    public class SaveConfigCommand<T> : ISaveConfigCommand<T> where T : new()
    {
        private readonly IConfigRepository<T> _configRepository;
        private readonly IDataRepository<T> _dataRepository;
        private readonly IDataRepository<Server> _serverRepository;

        public SaveConfigCommand(
            IConfigRepository<T> configRepository,
            IDataRepository<T> dataRepository,
            IDataRepository<Server> serverRepository)
        {
            _configRepository = configRepository;
            _dataRepository = dataRepository;
            _serverRepository = serverRepository;
        }

        public async Task ExecuteAsync(Guid serverId, T config)
        {
            var server = await _serverRepository.GetAsync(serverId);
            
            /*
            _configRepository.Save();
            _configRepository.Save(path, config);
            */
            throw new NotImplementedException();
        }
    }
}
