using AccServerAdmin.Domain;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Persistence.Server;
using AccServerAdmin.Application.Servers.Commands;

namespace AccServerAdmin.Tests.Application.Servers.Commands
{
    [ExcludeFromCodeCoverage]
    public class UpdateServerCommandTest
    {
        [Test]
        public void TestExecute()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var serverName = "TestFoo Server";
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var server = new Server {Id = serverId, Name = serverName, Location = serverPath};

            var repo = Substitute.For<IServerRepository>();
            var serverResolver = Substitute.For<IServerDirectoryResolver>();
            
            repo.Read(serverPath).Returns(server);
            serverResolver.Resolve(serverId).Returns(Path.Combine(settings.InstanceBasePath, serverId.ToString()));

            var command = new UpdateServerCommand(serverResolver, repo);

            // Act
            command.Execute(serverId, serverName);

            // Assert
            serverResolver.Received().Resolve(serverId);
            repo.Received().Read(serverPath);
            repo.Save(server);

        }

    }
}
