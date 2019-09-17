using Microsoft.Extensions.Options;

namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    using AccServerAdmin.Domain;
    using AccServerAdmin.Infrastructure.Helpers;
    using AccServerAdmin.Persistence.Server;
    

    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly IServerPersistence _serverPersistence;
        private readonly IServerSetup _serverSetup;
        private readonly AppSettings _settings;

        public CreateServerCommand(
            IServerPersistence serverPersistence,
            IServerSetup serverSetup,
            IOptions<AppSettings> settings)
        {
            _serverPersistence = serverPersistence;
            _serverSetup = serverSetup;
            _settings = settings.Value;
        }

        public Server Execute(string serverName)
        {
            var server = new Server { Name = serverName };

            _serverPersistence.Save(server);
            _serverSetup.Execute(server);

            return server;
        }
    }
}
