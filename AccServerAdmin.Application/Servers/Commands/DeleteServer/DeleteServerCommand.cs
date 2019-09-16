using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;
using System;

namespace AccServerAdmin.Application.Servers.Commands.DeleteServer
{
    public class DeleteServerCommand : IDeleteServerCommand
    {
        private readonly AppSettings _settings;

        public DeleteServerCommand(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public void Execute(Guid serverId)
        {
            throw new System.NotImplementedException();
        }
    }
}
