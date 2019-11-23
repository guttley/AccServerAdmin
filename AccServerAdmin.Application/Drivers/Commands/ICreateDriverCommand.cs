using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public interface ICreateDriverCommand
    {
        Task<Driver> ExecuteAsync(Driver driver);
    }
}
