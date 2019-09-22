using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.GameConfig;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Persistence
{
    [ExcludeFromCodeCoverage]
    public class GameConfigRepositoryTests
    {
        [Test]
        public void Test_New()
        {
            // Arrange
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();

            var repo = new GameConfigRepository(directory, file, converter);

            // Act 
            var config = repo.New();

            // Assert
            Assert.That(config, Is.Not.Null);

            Assert.That(config.ServerName, Is.EqualTo(GameConfigRepository.DefaultServerName));
            Assert.That(config.Password, Is.EqualTo(string.Empty));
            Assert.That(config.AdminPassword, Is.EqualTo(string.Empty));
            Assert.That(config.TrackMedalsRequirement, Is.EqualTo(GameConfigRepository.DefaultTrackMedalsRequirement));
            Assert.That(config.Version, Is.EqualTo(GameConfigRepository.DefaultConfigVersion));
            Assert.That(config.RacecraftRatingRequirement, Is.EqualTo(GameConfigRepository.DefaultRacecraftRatingRequirement));
            Assert.That(config.SpectatorSlots, Is.EqualTo(GameConfigRepository.DefaultSpectatorSlots));
            Assert.That(config.SpectatorPassword, Is.EqualTo(string.Empty));
            Assert.That(config.DumpLeaderboards, Is.EqualTo(GameConfigRepository.DefaultDumpLeaderboards));
            Assert.That(config.IsRaceLocked, Is.EqualTo(GameConfigRepository.DefaultIsRaceLocked));
        }

        [Test]
        public void Test_ReadReturnsNewIfDoesNotExist()
        {
            // Arrange
            var path = "C:\\FakeInstancePath";
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var repo = new GameConfigRepository(directory, file, converter);

            file.Exists(Arg.Any<string>()).Returns(false);

            // Act 
            var config = repo.Read(path);

            // Assert
            Assert.That(config.ServerName, Is.EqualTo(GameConfigRepository.DefaultServerName));
            Assert.That(config.Password, Is.EqualTo(string.Empty));
            Assert.That(config.AdminPassword, Is.EqualTo(string.Empty));
            Assert.That(config.TrackMedalsRequirement, Is.EqualTo(GameConfigRepository.DefaultTrackMedalsRequirement));
            Assert.That(config.Version, Is.EqualTo(GameConfigRepository.DefaultConfigVersion));
            Assert.That(config.RacecraftRatingRequirement, Is.EqualTo(GameConfigRepository.DefaultRacecraftRatingRequirement));
            Assert.That(config.SpectatorSlots, Is.EqualTo(GameConfigRepository.DefaultSpectatorSlots));
            Assert.That(config.SpectatorPassword, Is.EqualTo(string.Empty));
            Assert.That(config.DumpLeaderboards, Is.EqualTo(GameConfigRepository.DefaultDumpLeaderboards));
            Assert.That(config.IsRaceLocked, Is.EqualTo(GameConfigRepository.DefaultIsRaceLocked));
        }
    }
}