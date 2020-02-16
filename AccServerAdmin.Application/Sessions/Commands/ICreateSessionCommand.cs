using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public interface ICreateSessionCommand
    {
        Task Execute(Guid serverId, SessionConfiguration server);
    }
}
