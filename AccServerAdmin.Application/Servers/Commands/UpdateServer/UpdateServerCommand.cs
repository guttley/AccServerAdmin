using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;
using System;

namespace AccServerAdmin.Application.Servers.Commands.UpdateServer
{
    public class UpdateServerCommand : IUpdateServerCommand
    {
        private readonly AppSettings _settings;

        public UpdateServerCommand(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }


        public void Execute(Guid serverId, string serverName)
        {
            throw new System.NotImplementedException();
        }
    }
}
