using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Sessions.Commands
{
    public interface IDeleteSessionCommand
    {
        Task Execute(Guid sessionId);
    }
}
