namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    public interface ICreateServerCommand
    {
        void Execute(string serverName);
    }
}
