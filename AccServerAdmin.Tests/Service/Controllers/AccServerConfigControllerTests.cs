using System;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.ServerConfig.Commands;
using AccServerAdmin.Application.ServerConfig.Queries;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Service.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Service.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AccServerConfigControllerTests
    {
        [Test]
        public void GetsServerConfigById()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var saveCommand = Substitute.For<ISaveServerConfigCommand>();
            var getByIdCommand = Substitute.For<IGetServerConfigByIdQuery>();
            var controller = new AccServerConfigController(saveCommand, getByIdCommand);
            var config = new Configuration();

            getByIdCommand.Execute(Arg.Is(serverId)).Returns(config);

            // Act
            var returnedConfig = controller.GetServerConfig(serverId);

            // Assert
            getByIdCommand.Received().Execute(serverId);
            Assert.That(returnedConfig, Is.SameAs(config));
        }

        [Test]
        public void SaveServer()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var saveCommand = Substitute.For<ISaveServerConfigCommand>();
            var getByIdCommand = Substitute.For<IGetServerConfigByIdQuery>();
            var controller = new AccServerConfigController(saveCommand, getByIdCommand);
            var config = new Configuration();

            // Act
            controller.SaveServeConfig(serverId, config);

            // Assert
            saveCommand.Received().Execute(serverId, config);
        }


    }
}