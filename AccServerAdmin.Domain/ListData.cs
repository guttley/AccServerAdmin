using System;
using System.Collections.Generic;
using System.Linq;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// Holds data for selects
    /// </summary>
    public static class ListData
    {
        static ListData()
        {
            Ratings = new Dictionary<int, string>();

            for (var i = -1; i < 100; i++)
            {
                Ratings.Add(i, i.ToString());
            }

            Cars = EnumHelper.GetValues<CarModel>()
                .OrderBy(p => p)
                .ToDictionary(model => model, model => model.GetDescription());
        }

        public static Guid AnonymousDriverId { get; } = Guid.Parse("00000000-0000-0000-0000-000000000001");

        public static Dictionary<CarModel, string> Cars { get; }

        public static Dictionary<string, string> Tracks { get; } = new Dictionary<string, string>
        {
            {"monza", "Monza" },
            {"zolder", "Zolder" },
            {"brands_hatch", "Brands Hatch" },
            {"silverstone", "Silverstone" },
            {"paul_ricard", "Paul Ricard" },
            {"misano", "Misano" },
            {"spa", "Spa-Francorchamps" },
            {"nurburgring", "Nurburgring" },
            {"barcelona", "Barcelona" },
            {"hungaroring", "Hungaroring" },
            {"zandvoort", "Zandvoort" },
            {"monza_2019", "Monza 2019" },
            {"zolder_2019", "Zolder 2019" },
            {"brands_hatch_2019", "Brands Hatch 2019" },
            {"silverstone_2019", "Silverstone 2019" },
            {"paul_ricard_2019", "Paul Ricard 2019" },
            {"misano_2019", "Misano 2019" },
            {"spa_2019", "Spa-Francorchamps 2019" },
            {"nurburgring_2019", "Nurburgring 2019" },
            {"barcelona_2019", "Barcelona 2019" },
            {"hungaroring_2019", "Hungaroring 2019" },
            {"zandvoort_2019", "Zandvoort 2019" },
            {"kyalami_2019", "Kyalami 2019" },
            {"mount_panorama_2019", "Mount Panorama 2019" },
            {"suzuka_2019", "Suzuka 2019" },
            {"laguna_seca_2019", "Laguna Seca 2019" },
        };

        public static Dictionary<int, string> DriverTypes { get; } = new Dictionary<int, string>
        {
            { (int)DriverCategory.Bronze, "Bronze"},
            { (int)DriverCategory.Silver, "Silver"},
            { (int)DriverCategory.Gold, "Gold"},
            { (int)DriverCategory.Platinum, "Platinum"},
        };

        public static Dictionary<string, string> EventTypes { get; } = new Dictionary<string, string>
        {
            {"E_3h", "Endurance 3 Hour"},
            {"E_6h", "Endurance 6 Hour"},
            {"E_24h", "Endurance 24 Hour"},
            {"Sprint", "Sprint"}
        };

        public static Dictionary<FormationLap, string> FormationLapTypes { get; } = new Dictionary<FormationLap, string>
        {
            {FormationLap.Free, "Free (same as /manual)"},
            {FormationLap.Limited, "Limited speed lap"},
            {FormationLap.Default, "Default with position control and UI"}
        };

        public static Dictionary<int, string> TrackMedals { get; } = new Dictionary<int, string>
        {
            { 0, "0"},
            { 1, "1"},
            { 2, "2"},
            { 3, "3"},
        };

        public static Dictionary<int, string> Ratings { get; }

        public static Dictionary<SessionType, string> SessionTypes { get; } = new Dictionary<SessionType, string>
        {
            { SessionType.Practice, "Practice" },
            { SessionType.Qually, "Qualification" },
            { SessionType.Race, "Race" },
        };

        public static Dictionary<string, string> ResultSessionTypes { get; } = new Dictionary<string, string>
        {
            { "FP", "Free Practice" },
            { "Q", "Qualifying" },
            { "R", "Race" },
            { "FP1", "Free Practice 1" },
            { "FP2", "Free Practice 2" },
            { "Q2", "Qualifying 2" },
            { "R2", "Race 2" },
        };

        public static Dictionary<int, string> SessionDays { get; } = new Dictionary<int, string>
        {
            { 1, "Friday" },
            { 2, "Saturday" },
            { 3, "Sunday" },
        };
    }
}

