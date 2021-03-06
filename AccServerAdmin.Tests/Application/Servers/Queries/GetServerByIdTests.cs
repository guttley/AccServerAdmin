﻿using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Application.Servers.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetServerByIdTests
    { 
        [Test]
        public void TestExecute()
        {
            /*
            // Arrange
            var serverId = Guid.NewGuid();
            var serverName = "TestFoo Server";
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var server = new Server {Id = serverId, Name = serverName};
            var serverResolver = Substitute.For<IServerDirectoryResolver>();
            var repo = Substitute.For<IServerRepository>();

            serverResolver.Resolve(serverId).Returns(serverPath);
            repo.Read(serverPath).Returns(server);

            var command = new GetServerByIdQuery(serverResolver, repo);

            // Act
            var returnedServer = command.Execute(serverId);

            // Assert
            Assert.That(returnedServer, Is.EqualTo(server));
            serverResolver.Received().Resolve(serverId);
            repo.Received().Read(serverPath);
            */
        }
    }
}
