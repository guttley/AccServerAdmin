using System;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class GetConfigByIdTests
    {
        [Test]
        public void Executes() 
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var path = "C:\\MyFakePath";
            var resolver = Substitute.For<IServerDirectoryResolver>();
            var repo = Substitute.For<IConfigRepository<GameConfiguration>>();
            var command = new GetConfigByIdQuery<GameConfiguration>(resolver, repo);
            var config = new GameConfiguration();

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
