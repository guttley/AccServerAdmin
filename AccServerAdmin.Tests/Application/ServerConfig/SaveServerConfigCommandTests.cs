using System;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.ServerConfig.Commands;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.ServerConfig;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Application.ServerConfig
{
    [ExcludeFromCodeCoverage]
    public class SaveServerConfigCommandTests
    {

        [Test]
        public void Executes()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var path = "C:\\MyFakePath";
            var resolver = Substitute.For<IServerDirectoryResolver>();
            var repo = Substitute.For<IServerConfigRepository>();
            var command = new SaveServerConfigCommand(resolver, repo);
            var config = new Configuration();

            resolver.Resolve(serverId).Returns(path);


            // Act
            command.Execute(serverId, config);

            // Assert
            resolver.Received().Resolve(serverId);
            repo.Received().Save(path, config);
        }

    }
}
