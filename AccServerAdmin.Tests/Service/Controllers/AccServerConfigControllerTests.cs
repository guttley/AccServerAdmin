//using AccServerAdmin.Service.Controllers.Api;

namespace AccServerAdmin.Tests.Service.Controllers
{
    /*
    [ExcludeFromCodeCoverage]
    public class AccServerConfigControllerTests
    {
        [Test]
        public void GetsServerConfigById()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var saveCommand = Substitute.For<ISaveConfigCommand<ServerConfiguration>>();
            var getByIdCommand = Substitute.For<IGetConfigByIdQuery<ServerConfiguration>>();
            var controller = new AccServerConfigController(saveCommand, getByIdCommand);
            var config = new ServerConfiguration();

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
            var saveCommand = Substitute.For<ISaveConfigCommand<ServerConfiguration>>();
            var getByIdCommand = Substitute.For<IGetConfigByIdQuery<ServerConfiguration>>();
            var controller = new AccServerConfigController(saveCommand, getByIdCommand);
            var config = new ServerConfiguration();

            // Act
            controller.SaveServerConfig(serverId, config);

            // Assert
            saveCommand.Received().Execute(serverId, config);
        }
    }
    */
}