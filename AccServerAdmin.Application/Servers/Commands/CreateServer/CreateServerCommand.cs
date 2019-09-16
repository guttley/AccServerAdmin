using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;
using System.IO;

namespace AccServerAdmin.Application.Servers.Commands.CreateServer
{
    public class CreateServerCommand : ICreateServerCommand
    {
        private readonly AppSettings _settings;

        public CreateServerCommand(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public Server Execute(string serverName)
        {
            var server = new Server { Name = serverName };

            

            //
            //  Create a new directory in the 
            //


            return server;
        }
    }
}
