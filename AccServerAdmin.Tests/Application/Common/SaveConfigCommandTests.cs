﻿using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Tests.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class SaveServerConfigCommandTests
    {
        /*
        [Test]
        public void Executes()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var path = "C:\\MyFakePath";
            var resolver = Substitute.For<IServerDirectoryResolver>();
            var repo = Substitute.For<IConfigRepository<ServerConfiguration>>();
            var command = new SaveConfigCommand<ServerConfiguration>(resolver, repo);
            var config = new ServerConfiguration();

            resolver.Resolve(serverId).Returns(path);

            // Act
            command.Execute(serverId, config);

            // Assert
            resolver.Received().Resolve(serverId);
            repo.Received().Save(path, config);
        }
        */
    }
}
