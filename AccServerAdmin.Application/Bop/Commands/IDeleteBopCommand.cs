using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Bop.Commands
{
    public interface IDeleteBopCommand
    {
        Task ExecuteAsync(Guid bopId);
    }
}
