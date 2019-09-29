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
        private readonly IServerRepository _serverRepository;

        public SaveConfigCommand(
            IConfigRepository<T> configRepository,
            IDataRepository<T> dataRepository,
            IServerRepository serverRepository)
        {
            _configRepository = configRepository;
            _dataRepository = dataRepository;
            _serverRepository = serverRepository;
        }

        public async Task ExecuteAsync(Guid serverId, T config)
        {
            var server = await _serverRepository.GetAsync(serverId).ConfigureAwait(false); 
            
            /*
            _configRepository.Save();
            _configRepository.Save(path, config);
            */
            throw new NotImplementedException();
        }
    }
}
