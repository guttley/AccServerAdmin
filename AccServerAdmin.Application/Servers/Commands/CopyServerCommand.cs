using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Domain.Results;

namespace AccServerAdmin.Application.Servers.Commands
{
    using AccServerAdmin.Domain;
    using AccServerAdmin.Persistence.Common;
    using AccServerAdmin.Persistence.Repository;
    
    public class CopyServerCommand : ICopyServerCommand
    {
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CopyServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Server> Execute(Guid serverId)
        {
            var server = await _serverRepository.Get(serverId);

            _unitOfWork.DetachEntity(server, nameof(Server.Id));
            server.Name = $"{server.Name} - COPY";

            _unitOfWork.DetachEntity(server.AssistRules, nameof(AssistRules.Id), nameof(AssistRules.ServerId));
            _unitOfWork.DetachEntity(server.EventCfg, nameof(EventCfg.Id), nameof(EventCfg.ServerId));
            _unitOfWork.DetachEntity(server.EventRules, nameof(EventRules.Id), nameof(EventRules.ServerId));
            _unitOfWork.DetachEntity(server.EntryList, nameof(EntryList.Id), nameof(EntryList.ServerId));
            _unitOfWork.DetachEntity(server.GameCfg, nameof(GameCfg.Id), nameof(GameCfg.ServerId));
            _unitOfWork.DetachEntity(server.NetworkCfg, nameof(NetworkCfg.Id), nameof(NetworkCfg.ServerId));
            
            server.EventCfg.Sessions.ForEach(s => _unitOfWork.DetachEntity(s, nameof(SessionConfiguration.Id), nameof(SessionConfiguration.EventCfgId)));
            server.EntryList.Entries.ForEach(e => _unitOfWork.DetachEntity(e, nameof(Entry.Id), nameof(Entry.EntryListId)));
            server.ServerBop.ForEach(b => _unitOfWork.DetachEntity(b, nameof(BalanceOfPerformance.Id), nameof(BalanceOfPerformance.ServerId)));
            
            await _serverRepository.Add(server);
            await _unitOfWork.SaveChanges();

            return server;
        }

    }
}