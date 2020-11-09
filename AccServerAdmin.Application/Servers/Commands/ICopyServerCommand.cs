using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface ICopyServerCommand
    {
        Task<Server> Execute(Guid serverId);
    }
}
