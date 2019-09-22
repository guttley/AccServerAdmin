using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Infrastructure.IO;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class DeleteServerCommand : IDeleteServerCommand
    {
        private readonly IDirectory _directory;
        private readonly IServerDirectoryResolver _serverResolver;

        public DeleteServerCommand(
            IServerDirectoryResolver serverResolver,
            IDirectory directory)
        {
            _serverResolver = serverResolver;
            _directory = directory;
        }

        public void Execute(Guid serverId)
        {
            var path = _serverResolver.Resolve(serverId);
            _directory.Delete(path, true);
        }
    }
}
