using System;

namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    public interface IDeleteServerCommand
    {
        void Execute(Guid serverId);
    }
}
