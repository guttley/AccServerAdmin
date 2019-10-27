using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public interface ICreateSessionCommand
    {
        Task ExecuteAsync(Guid serverId, SessionConfiguration server);
    }
}
