using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Common
{
    public interface IServerConfigWriter
    {
        void Execute(Server server, string serverPath);
    }
}
