using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.Servers.Commands
{
    public class DeleteServerCommand : IDeleteServerCommand
    {
        private readonly IDirectory _directory;
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServerDirectoryResolver _serverResolver;

        public DeleteServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork,
            IServerDirectoryResolver serverResolver,
            IDirectory directory)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
            _serverResolver = serverResolver;
            _directory = directory;
        }

        public async Task ExecuteAsync(Guid serverId)
        {
            var path = await _serverResolver.ResolveAsync(serverId).ConfigureAwait(false);
            _directory.Delete(path, true);
            _serverRepository.Delete(serverId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
