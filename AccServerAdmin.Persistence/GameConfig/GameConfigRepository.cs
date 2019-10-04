using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.GameConfig
{
    /// <summary>
    /// Implements IServerConfigRepository
    /// </summary>
    public class GameConfigRepository : BaseConfigRepository<GameConfiguration>
    {
        private const string ConfigDir = "cfg";
        private const string Filename = "settings.json";

        public const string DefaultServerName = "New Server";
        public const int DefaultTrackMedalsRequirement = 3;
        public const int DefaultConfigVersion = 1;
        public const int DefaultRacecraftRatingRequirement = 70;
        public const int DefaultSafteyRatingRequirement = 0;
        public const int DefaultSpectatorSlots = 0;
        public const bool DefaultDumpLeaderboards = false;
        public const bool DefaultDumpEntryList = false;
        public const bool DefaultIsRaceLocked = false;
        public const bool DefaultAutoDq = false;
        public const bool DefaultShortFormationLap = false;
        public const bool DefaultRandomTrackWhenEmpty = false;

        public GameConfigRepository(
            IDirectory directory,
            IFile file,
            IJsonConverter converter) 
            : base(directory, file, converter, ConfigDir, Filename)
        {
        }

        /// <inheritdoc />
        public override GameConfiguration New()
        {
            return new GameConfiguration
            {
                ServerName = DefaultServerName,
                Password = string.Empty,
                AdminPassword = string.Empty,
                TrackMedalsRequirement = DefaultTrackMedalsRequirement,
                Version = DefaultConfigVersion,
                RacecraftRatingRequirement = DefaultRacecraftRatingRequirement,
                SafetyRatingRequirement = DefaultSafteyRatingRequirement,
                SpectatorSlots = DefaultSpectatorSlots,
                SpectatorPassword = string.Empty,
                DumpLeaderboards = DefaultDumpLeaderboards,
                DumpEntryList = DefaultDumpEntryList,
                IsRaceLocked = DefaultIsRaceLocked,
                AllowAutoDisqualification = DefaultAutoDq,
                ShortFormationLap = DefaultShortFormationLap,
                RandomizeTrackWhenEmpty = DefaultRandomTrackWhenEmpty
            };
        }
    }
}
