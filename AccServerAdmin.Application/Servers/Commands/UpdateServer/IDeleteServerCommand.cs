using System;

namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    public interface IUpdateServerCommand
    {
        void Execute(Guid serverId, string serverName);
    }
}
