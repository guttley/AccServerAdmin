using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Common
{
    public class SaveConfigCommand<T> : ISaveConfigCommand<T> where T : class, IKeyedEntity
    {
        //private readonly IConfigRepository<T> _configRepository;
        private readonly IDataRepository<T> _dataRepository;
        private readonly IServerRepository _serverRepository;

        public SaveConfigCommand(
            //IConfigRepository<T> configRepository,
            IDataRepository<T> dataRepository,
            IServerRepository serverRepository)
        {
            //_configRepository = configRepository;
            _dataRepository = dataRepository;
            _serverRepository = serverRepository;
        }

        public Task ExecuteAsync(Guid serverId, T config)
        {
            
            /*
            _configRepository.Save();
            _configRepository.Save(path, config);
            */
            throw new NotImplementedException();
        }
    }
}
