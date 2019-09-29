using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface ICreateServerCommand
    {
        Task<Server> ExecuteAsync(string serverName);
    }
}
