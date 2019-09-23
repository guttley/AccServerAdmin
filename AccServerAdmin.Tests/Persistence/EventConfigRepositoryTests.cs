using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.EventConfig;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Persistence
{
    [ExcludeFromCodeCoverage]
    public class EventConfigRepositoryTests
    {
        [Test]
        public void Test_New()
        {
            // Arrange
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();

            var repo = new EventConfigRepository(directory, file, converter);

            // Act 
            var config = repo.New();

            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.AmbientTemp, Is.EqualTo(EventConfigRepository.DefaultAmbientTemp));
            Assert.That(config.CloudLevel, Is.EqualTo(EventConfigRepository.DefaultCloudLevel));
            Assert.That(config.EventType, Is.EqualTo(EventConfigRepository.DefaultEventType));
            Assert.That(config.PostQualySeconds, Is.EqualTo(EventConfigRepository.DefaultPostQualySeconds));
            Assert.That(config.PreRaceWaitingTimeSeconds, Is.EqualTo(EventConfigRepository.DefaultPreRaceWaitingTimeSeconds));
            Assert.That(config.Rain, Is.EqualTo(EventConfigRepository.DefaultRain));
            Assert.That(config.PostRaceSeconds, Is.EqualTo(EventConfigRepository.DefaultPostRaceSeconds));
            Assert.That(config.SessionOverTimeSeconds, Is.EqualTo(EventConfigRepository.DefaultSessionOverTimeSeconds));
            Assert.That(config.Track, Is.EqualTo(EventConfigRepository.DefaultTrack));
            Assert.That(config.TrackTemp, Is.EqualTo(EventConfigRepository.DefaultTrackTemp));
            Assert.That(config.WeatherRandomness, Is.EqualTo(EventConfigRepository.DefaultWeatherRandomness));
            Assert.That(config.Version, Is.EqualTo(EventConfigRepository.DefaultConfigVersion));
        }

        [Test]
        public void Test_ReadReturnsNewIfDoesNotExist()
        {
            // Arrange
            var path = "C:\\FakeInstancePath";
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var repo = new EventConfigRepository(directory, file, converter);

            file.Exists(Arg.Any<string>()).Returns(false);

            // Act 
            var config = repo.Read(path);

            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.AmbientTemp, Is.EqualTo(EventConfigRepository.DefaultAmbientTemp));
            Assert.That(config.CloudLevel, Is.EqualTo(EventConfigRepository.DefaultCloudLevel));
            Assert.That(config.EventType, Is.EqualTo(EventConfigRepository.DefaultEventType));
            Assert.That(config.PostQualySeconds, Is.EqualTo(EventConfigRepository.DefaultPostQualySeconds));
            Assert.That(config.PreRaceWaitingTimeSeconds, Is.EqualTo(EventConfigRepository.DefaultPreRaceWaitingTimeSeconds));
            Assert.That(config.Rain, Is.EqualTo(EventConfigRepository.DefaultRain));
            Assert.That(config.PostRaceSeconds, Is.EqualTo(EventConfigRepository.DefaultPostRaceSeconds));
            Assert.That(config.SessionOverTimeSeconds, Is.EqualTo(EventConfigRepository.DefaultSessionOverTimeSeconds));
            Assert.That(config.Track, Is.EqualTo(EventConfigRepository.DefaultTrack));
            Assert.That(config.TrackTemp, Is.EqualTo(EventConfigRepository.DefaultTrackTemp));
            Assert.That(config.WeatherRandomness, Is.EqualTo(EventConfigRepository.DefaultWeatherRandomness));
            Assert.That(config.Version, Is.EqualTo(EventConfigRepository.DefaultConfigVersion));
        }
    }
}