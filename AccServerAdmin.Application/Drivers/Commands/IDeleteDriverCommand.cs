using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public interface IDeleteDriverCommand
    {
        Task Execute(Guid driverId);
    }
}
