using System;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Application.ServerConfig
{
    [ExcludeFromCodeCoverage]
    public class GetServerConfigByIdTests
    {

        [Test]
        public void Executes()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var path = "C:\\MyFakePath";
            var resolver = Substitute.For<IServerDirectoryResolver>();
            var repo = Substitute.For<IConfigRepository<ServerConfiguration>>();
            var command = new GetConfigByIdQuery<ServerConfiguration>(resolver, repo);
            var config = new ServerConfiguration();

            resolver.Resolve(serverId).Returns(path);
            repo.Read(path).Returns(config);

            // Act
            var returnedConfig = command.Execute(serverId);

            // Assert
            resolver.Received().Resolve(serverId);
            repo.Received().Read(path);
            Assert.That(returnedConfig, Is.EqualTo(config));
        }

    }
}
