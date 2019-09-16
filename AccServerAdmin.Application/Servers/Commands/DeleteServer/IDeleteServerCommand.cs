using System;

namespace AccServerAdmin.Application.Servers.Commands.DeleteServer
{
    public interface IDeleteServerCommand
    {
        void Execute(Guid serverId);
    }
}
