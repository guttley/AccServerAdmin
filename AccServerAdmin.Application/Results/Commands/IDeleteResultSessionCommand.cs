using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Results.Commands
{
    public interface IDeleteResultSessionCommand
    {
        Task Execute(Guid sessionId);
    }
}
