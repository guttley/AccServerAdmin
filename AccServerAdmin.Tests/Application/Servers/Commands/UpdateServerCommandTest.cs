using AccServerAdmin.Domain;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Tests.Application.Servers.Commands
{
    [ExcludeFromCodeCoverage]
    public class UpdateServerCommandTest
    {
        /*
        [Test]
        public async Task TestExecute()
        {
            
            // Arrange
            var serverId = Guid.NewGuid();
            var serverName = "TestFoo Server";
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var server = new Server {Id = serverId, Name = serverName};
            var updatedServer = new Server {Id = serverId, Name = "New Test"};
            var repo = Substitute.For<IDataRepository<Server>>();
            
            repo.GetAsync(serverId).Returns(server);

            var command = new UpdateServerCommand(repo);

            // Act
            await command.ExecuteAsync(updatedServer);

            // Assert
            serverResolver.Received().Resolve(serverId);
            repo.Received().Read(serverPath);
            repo.Save(server);
            
        }
        */
    }
}
