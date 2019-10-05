using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Common
{
    public interface IServerInstanceCreator
    {
        Task ExecuteAsync(Server server);
    }
}
