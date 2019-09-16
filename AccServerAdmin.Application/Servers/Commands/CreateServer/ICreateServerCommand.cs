using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    public interface ICreateServerCommand
    {
        Server Execute(string serverName);
    }
}
