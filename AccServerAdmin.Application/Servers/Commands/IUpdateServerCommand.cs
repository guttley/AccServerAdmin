using System;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface IUpdateServerCommand
    {
        void Execute(Guid serverId, string serverName);
    }
}
