using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Application.Servers.Commands.UpdateServer;
using AccServerAdmin.Persistence.Server;

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
            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                serverPath
            };

            var options = Substitute.For<IOptions<AppSettings>>();
            var repo = Substitute.For<IServerRepository>();
            var directory = Substitute.For<IDirectory>();

            options.Value.Returns(settings);
            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);
            repo.Read(serverPath).Returns(server);

            var command = new UpdateServerCommand(options, repo, directory);

            // Act
            command.Execute(serverId, serverName);

            // Assert
            directory.Received().GetDirectories(settings.InstanceBasePath);
            repo.Received().Read(serverPath);
            repo.Save(server);

        }

        [Test]
        public void TestExecute_ThrowsKeyNotFound()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var serverName = "TestFoo Server";
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
            };

            var options = Substitute.For<IOptions<AppSettings>>();
            var repo = Substitute.For<IServerRepository>();
            var directory = Substitute.For<IDirectory>();

            options.Value.Returns(settings);
            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);

            var command = new UpdateServerCommand(options, repo, directory);

            // Act / Assert
            Assert.Throws<KeyNotFoundException>(() => command.Execute(serverId, serverName));
        }
    }
}
