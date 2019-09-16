using System;

namespace AccServerAdmin.Application.Servers.Commands.UpdateServer
{
    public interface IUpdateServerCommand
    {
        void Execute(Guid serverId, string serverName);
    }
}
