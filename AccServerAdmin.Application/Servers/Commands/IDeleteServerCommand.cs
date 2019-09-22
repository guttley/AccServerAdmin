using System;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface IDeleteServerCommand
    {
        void Execute(Guid serverId);
    }
}
