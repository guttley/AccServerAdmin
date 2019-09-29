using System;
using System.Threading.Tasks;
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

        public async Task ExecuteAsync(Guid serverId)
        {
            var path = await _serverResolver.ResolveAsync(serverId).ConfigureAwait(false);
            _directory.Delete(path, true);
        }
    }
}
