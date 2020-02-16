using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Tests.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class ServerDirectoryResolverTests
    {
        /*
        [Test]
        public void Resolves()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var settings = new AppSettings {ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath"};
            var options = Substitute.For<IDataRepository<AppSettings>>();
            var directory = Substitute.For<IDirectory>();

            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, serverId.ToString())
            };

            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);
            options.GetAll().Returns(new List<AppSettings> {settings});

            var serverResolver = new ServerDirectoryResolver(options, directory);

            // Act
            var path = serverResolver.Resolve(serverId);

            // Assert
            directory.GetDirectories(settings.InstanceBasePath);
            Assert.That(path, Is.EqualTo(Path.Combine(settings.InstanceBasePath, serverId.ToString())));
        }

        [Test]
        public void Resolve_ThrowsKeyNotFound()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath" };
            var options = Substitute.For<IDataRepository<AppSettings>>();
            var directory = Substitute.For<IDirectory>();

            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath , Guid.NewGuid().ToString()),
            };

            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);
            options.GetAll().Returns(new List<AppSettings> {settings});

            var serverResolver = new ServerDirectoryResolver(options, directory);

            // Act 
            Assert.Throws<KeyNotFoundException>(() => serverResolver.Resolve(serverId));

            // Assert
            directory.GetDirectories(settings.InstanceBasePath);
        }
        */
    }
}
