using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class SQlServerInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerBasePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstanceBasePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassphrase = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectResults = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarModel = table.Column<int>(type: "int", nullable: false),
                    CupCategory = table.Column<long>(type: "bigint", nullable: false),
                    RaceNumber = table.Column<long>(type: "bigint", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Track = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsWet = table.Column<bool>(type: "bit", nullable: false),
                    BestLap = table.Column<int>(type: "int", nullable: false),
                    BestSplit1 = table.Column<int>(type: "int", nullable: false),
                    BestSplit2 = table.Column<int>(type: "int", nullable: false),
                    BestSplit3 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssistRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StabilityControlLevelMax = table.Column<int>(type: "int", nullable: false),
                    DisableAutosteer = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutoLights = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutoWiper = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutoEngineStart = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutoPitLimiter = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutoGear = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutoClutch = table.Column<bool>(type: "bit", nullable: false),
                    DisableIdealLine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistRules_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BalanceOfPerformance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Track = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Car = table.Column<int>(type: "int", nullable: false),
                    Ballast = table.Column<int>(type: "int", nullable: false),
                    Restrictor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceOfPerformance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceOfPerformance_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForceEntryList = table.Column<bool>(type: "bit", nullable: false),
                    ConfigVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryList_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventCfgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Track = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreRaceWaitingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    SessionOverTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    AmbientTemp = table.Column<int>(type: "int", nullable: false),
                    CloudLevel = table.Column<double>(type: "float", nullable: false),
                    Rain = table.Column<double>(type: "float", nullable: false),
                    WeatherRandomness = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    PostQualySeconds = table.Column<int>(type: "int", nullable: false),
                    PostRaceSeconds = table.Column<int>(type: "int", nullable: false),
                    SimRacerWeatherConditions = table.Column<bool>(type: "bit", nullable: false),
                    isFixedConditionQualification = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCfgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCfgs_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QualifyType = table.Column<int>(type: "int", nullable: false),
                    PitWindowLength = table.Column<int>(type: "int", nullable: false),
                    DriverStintTime = table.Column<int>(type: "int", nullable: false),
                    MandatoryPitstopCount = table.Column<int>(type: "int", nullable: false),
                    MaxTotalDrivingTime = table.Column<int>(type: "int", nullable: false),
                    MaxDriversCount = table.Column<int>(type: "int", nullable: false),
                    RefuellingAllowedInRace = table.Column<bool>(type: "bit", nullable: false),
                    RefuellingTimeFixed = table.Column<bool>(type: "bit", nullable: false),
                    MandatoryPitstopRefuellingRequired = table.Column<bool>(type: "bit", nullable: false),
                    MandatoryPitstopTyreChangeRequired = table.Column<bool>(type: "bit", nullable: false),
                    MandatoryPitstopSwapDriverRequired = table.Column<bool>(type: "bit", nullable: false),
                    TyreSetCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRules_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameCfgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarGroup = table.Column<int>(type: "int", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackMedalsRequirement = table.Column<int>(type: "int", nullable: false),
                    SafetyRatingRequirement = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    RacecraftRatingRequirement = table.Column<int>(type: "int", nullable: false),
                    SpectatorPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowAutoDisqualification = table.Column<bool>(type: "bit", nullable: false),
                    RandomizeTrackWhenEmpty = table.Column<bool>(type: "bit", nullable: false),
                    ShortFormationLap = table.Column<bool>(type: "bit", nullable: false),
                    IsRaceLocked = table.Column<bool>(type: "bit", nullable: false),
                    DumpEntryList = table.Column<bool>(type: "bit", nullable: false),
                    DumpLeaderboards = table.Column<bool>(type: "bit", nullable: false),
                    MaxCarSlots = table.Column<int>(type: "int", nullable: false),
                    CentralEntryListPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormationLap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCfgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCfgs_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetworkCfgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UdpPort = table.Column<int>(type: "int", nullable: false),
                    TcpPort = table.Column<int>(type: "int", nullable: false),
                    MaxConnections = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    RegisterToLobby = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkCfgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkCfgs_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionPenalties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Penalty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenaltyValue = table.Column<int>(type: "int", nullable: false),
                    ViolationInLap = table.Column<int>(type: "int", nullable: false),
                    ClearedInLap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPenalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionPenalties_SessionCars_CarId",
                        column: x => x.CarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPenalties_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomCar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaceNumber = table.Column<int>(type: "int", nullable: false),
                    DefaultGridPosition = table.Column<int>(type: "int", nullable: false),
                    ForcedCarModel = table.Column<int>(type: "int", nullable: false),
                    OverrideDriverInfo = table.Column<bool>(type: "bit", nullable: false),
                    ServerAdmin = table.Column<bool>(type: "bit", nullable: false),
                    OverrideCarModelForCustomCar = table.Column<bool>(type: "bit", nullable: false),
                    Ballast = table.Column<int>(type: "int", nullable: false),
                    Restrictor = table.Column<int>(type: "int", nullable: false),
                    ConfigVersion = table.Column<int>(type: "int", nullable: false),
                    GlobalEntryListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_EntryList_EntryListId",
                        column: x => x.EntryListId,
                        principalTable: "EntryList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entries_GlobalEntries_GlobalEntryListId",
                        column: x => x.GlobalEntryListId,
                        principalTable: "GlobalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventCfgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourOfDay = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekend = table.Column<int>(type: "int", nullable: false),
                    TimeMultiplier = table.Column<int>(type: "int", nullable: false),
                    SessionType = table.Column<int>(type: "int", nullable: false),
                    SessionDurationMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionConfiguration_EventCfgs_EventCfgId",
                        column: x => x.EventCfgId,
                        principalTable: "EventCfgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shortname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverCategory = table.Column<int>(type: "int", nullable: false),
                    HelmetTemplateKey = table.Column<int>(type: "int", nullable: false),
                    HelmetBaseColor = table.Column<int>(type: "int", nullable: false),
                    HelmetDetailColor = table.Column<int>(type: "int", nullable: false),
                    HelmetMaterialType = table.Column<int>(type: "int", nullable: false),
                    HelmetGlassColor = table.Column<int>(type: "int", nullable: false),
                    HelmetGlassMetallic = table.Column<double>(type: "float", nullable: false),
                    GlovesTemplateKey = table.Column<int>(type: "int", nullable: false),
                    SuitTemplateKey = table.Column<int>(type: "int", nullable: false),
                    SuitDetailColor1 = table.Column<int>(type: "int", nullable: false),
                    SuitDetailColor2 = table.Column<int>(type: "int", nullable: false),
                    AiSkill = table.Column<int>(type: "int", nullable: false),
                    AiAggro = table.Column<int>(type: "int", nullable: false),
                    AiRainSkill = table.Column<int>(type: "int", nullable: false),
                    AiConsistency = table.Column<int>(type: "int", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverEntries",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverEntries", x => new { x.EntryId, x.DriverId });
                    table.ForeignKey(
                        name: "FK_DriverEntries_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverEntries_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentDriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastLap = table.Column<int>(type: "int", nullable: false),
                    LastSplit1 = table.Column<int>(type: "int", nullable: false),
                    LastSplit2 = table.Column<int>(type: "int", nullable: false),
                    LastSplit3 = table.Column<int>(type: "int", nullable: false),
                    BestLap = table.Column<int>(type: "int", nullable: false),
                    BestSplit1 = table.Column<int>(type: "int", nullable: false),
                    BestSplit2 = table.Column<int>(type: "int", nullable: false),
                    BestSplit3 = table.Column<int>(type: "int", nullable: false),
                    TotalTime = table.Column<int>(type: "int", nullable: false),
                    LapCount = table.Column<int>(type: "int", nullable: false),
                    MissingMandatoryPitstop = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderboardLines_Drivers_CurrentDriverId",
                        column: x => x.CurrentDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaderboardLines_SessionCars_CarId",
                        column: x => x.CarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaderboardLines_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionCarDrivers",
                columns: table => new
                {
                    SessionCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCarDrivers", x => new { x.DriverId, x.SessionCarId });
                    table.ForeignKey(
                        name: "FK_SessionCarDrivers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionCarDrivers_SessionCars_SessionCarId",
                        column: x => x.SessionCarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionLaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Lap = table.Column<int>(type: "int", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    LapTime = table.Column<long>(type: "bigint", nullable: false),
                    Split1 = table.Column<long>(type: "bigint", nullable: false),
                    Split2 = table.Column<long>(type: "bigint", nullable: false),
                    Split3 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionLaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionLaps_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionLaps_SessionCars_CarId",
                        column: x => x.CarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionLaps_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssistRules_ServerId",
                table: "AssistRules",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceOfPerformance_ServerId",
                table: "BalanceOfPerformance",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverEntries_DriverId",
                table: "DriverEntries",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_EntryId",
                table: "Drivers",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_PlayerId",
                table: "Drivers",
                column: "PlayerId",
                unique: true,
                filter: "[PlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_EntryListId",
                table: "Entries",
                column: "EntryListId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_GlobalEntryListId",
                table: "Entries",
                column: "GlobalEntryListId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryList_ServerId",
                table: "EntryList",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventCfgs_ServerId",
                table: "EventCfgs",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRules_ServerId",
                table: "EventRules",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameCfgs_ServerId",
                table: "GameCfgs",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardLines_CarId",
                table: "LeaderboardLines",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardLines_CurrentDriverId",
                table: "LeaderboardLines",
                column: "CurrentDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardLines_SessionId",
                table: "LeaderboardLines",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkCfgs_ServerId",
                table: "NetworkCfgs",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionCarDrivers_SessionCarId",
                table: "SessionCarDrivers",
                column: "SessionCarId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionConfiguration_EventCfgId",
                table: "SessionConfiguration",
                column: "EventCfgId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_CarId",
                table: "SessionLaps",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_DriverId",
                table: "SessionLaps",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_SessionId",
                table: "SessionLaps",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPenalties_CarId",
                table: "SessionPenalties",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPenalties_SessionId",
                table: "SessionPenalties",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionTimestamp",
                table: "Sessions",
                column: "SessionTimestamp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Track",
                table: "Sessions",
                column: "Track");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssistRules");

            migrationBuilder.DropTable(
                name: "BalanceOfPerformance");

            migrationBuilder.DropTable(
                name: "DriverEntries");

            migrationBuilder.DropTable(
                name: "EventRules");

            migrationBuilder.DropTable(
                name: "GameCfgs");

            migrationBuilder.DropTable(
                name: "LeaderboardLines");

            migrationBuilder.DropTable(
                name: "NetworkCfgs");

            migrationBuilder.DropTable(
                name: "SessionCarDrivers");

            migrationBuilder.DropTable(
                name: "SessionConfiguration");

            migrationBuilder.DropTable(
                name: "SessionLaps");

            migrationBuilder.DropTable(
                name: "SessionPenalties");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventCfgs");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "SessionCars");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "EntryList");

            migrationBuilder.DropTable(
                name: "GlobalEntries");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
