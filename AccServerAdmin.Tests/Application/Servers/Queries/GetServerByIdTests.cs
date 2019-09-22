using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Application.ServerConfig.Queries;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Application.Servers.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetServerByIdTests
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

            var command = new GetServerByIdQuery(options, repo, directory);

            // Act
            var returnedServer = command.Execute(serverId);

            // Assert
            Assert.That(returnedServer, Is.EqualTo(server));
            directory.Received().GetDirectories(settings.InstanceBasePath);
            repo.Received().Read(serverPath);
        }

        [Test]
        public void TestExecute_ThrowsKeyNotFound()
        {
            // Arrange
            var serverId = Guid.NewGuid();
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

            var command = new GetServerByIdQuery(options, repo, directory);

            // Act / Assert
            Assert.Throws<KeyNotFoundException>(() => command.Execute(serverId));
        }
    }
}
