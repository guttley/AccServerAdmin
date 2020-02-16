using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Common
{
    public interface IServerConfigWriter
    {
        Task Execute(Server server, string serverPath);
    }
}
