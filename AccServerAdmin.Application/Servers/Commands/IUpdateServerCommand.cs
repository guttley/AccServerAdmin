using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface IUpdateSessionCommand
    {
        Task ExecuteAsync(Server server);
    }
}
