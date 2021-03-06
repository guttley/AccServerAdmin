﻿using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Servers.Commands
{
    using AccServerAdmin.Domain;

    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly IServerRepository _serverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServerCommand(
            IServerRepository serverRepository,
            IUnitOfWork unitOfWork)
        {
            _serverRepository = serverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Server> Execute(string serverName)
        {
            var server = new Server {Name = serverName};

            if (await _serverRepository.IsUniqueNameAsync(serverName))
            {
                throw new SteamIdNotUniqueException("Server names must be unique");
            }

            await _serverRepository.Add(server);
            await _unitOfWork.SaveChanges();

            return server;
        }
    }
}