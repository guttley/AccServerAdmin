using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Common
{
    public interface IServerConfigWriter
    {
        Task ExecuteAsync(Server server);
    }
}
