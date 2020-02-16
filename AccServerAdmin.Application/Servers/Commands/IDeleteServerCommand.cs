using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface IDeleteServerCommand
    {
        Task Execute(Guid serverId);
    }
}
