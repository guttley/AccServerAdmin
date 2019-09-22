using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Server;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetServerByIdQuery : IGetServerByIdQuery
    {
        private readonly IServerRepository _serverRepository;
        private readonly IServerDirectoryResolver _serverResolver;

        public GetServerByIdQuery(
            IServerDirectoryResolver serverResolver,
            IServerRepository serverRepository)
        {
            _serverResolver = serverResolver;
            _serverRepository = serverRepository;
        }
        
        public Server Execute(Guid serverId)
        {
            var path = _serverResolver.Resolve(serverId);
            var server = _serverRepository.Read(path);

            return server;
        }
    }
}
