using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface ICreateServerCommand
    {
        Server Execute(string serverName);
    }
}
