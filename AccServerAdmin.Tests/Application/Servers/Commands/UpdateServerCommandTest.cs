using System.Diagnostics.CodeAnalysis;

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
            var repo = Substitute.For<IServerRepository>();
            
            repo.GetAsync(serverId).Returns(server);

            var command = new UpdateServerCommand(repo);

            // Act
            await command.Execute(updatedServer);

            // Assert
            serverResolver.Received().Resolve(serverId);
            repo.Received().Read(serverPath);
            repo.Save(server);
            
        }
        */
    }
}
