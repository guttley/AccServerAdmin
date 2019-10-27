using System;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Service.Controllers
{
    /*
    [ExcludeFromCodeCoverage]
    public class AccEventConfigControllerTests
    {
        [Test]
        public void GetsGameConfigById()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var saveCommand = Substitute.For<ISaveConfigCommand<EventCfg>>();
            var getByIdCommand = Substitute.For<IGetConfigByIdQuery<EventCfg>>();
            var controller = new AccEventConfigController(saveCommand, getByIdCommand);
            var config = new EventCfg();

            getByIdCommand.Execute(Arg.Is(serverId)).Returns(config);

            // Act
            var returnedConfig = controller.GetGameConfig(serverId);

            // Assert
            getByIdCommand.Received().Execute(serverId);
            Assert.That(returnedConfig, Is.SameAs(config));
        }

        [Test]
        public void SaveServer()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var saveCommand = Substitute.For<ISaveConfigCommand<EventCfg>>();
            var getByIdCommand = Substitute.For<IGetConfigByIdQuery<EventCfg>>();
            var controller = new AccEventConfigController(saveCommand, getByIdCommand);
            var config = new EventCfg();

            // Act
            controller.SaveGameConfig(serverId, config);

            // Assert
            saveCommand.Received().Execute(serverId, config);
        }
    }
    */
}