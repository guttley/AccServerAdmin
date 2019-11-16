using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServerBasePath = table.Column<string>(nullable: false),
                    InstanceBasePath = table.Column<string>(nullable: false),
                    AdminPassphrase = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "EventCfgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    Track = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: true),
                    PreRaceWaitingTimeSeconds = table.Column<int>(nullable: false),
                    SessionOverTimeSeconds = table.Column<int>(nullable: false),
                    AmbientTemp = table.Column<int>(nullable: false),
                    CloudLevel = table.Column<double>(nullable: false),
                    Rain = table.Column<double>(nullable: false),
                    WeatherRandomness = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    PostQualySeconds = table.Column<int>(nullable: false),
                    PostRaceSeconds = table.Column<int>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    QualifyType = table.Column<int>(nullable: false),
                    PitWindowLength = table.Column<int>(nullable: false),
                    DriverStintTime = table.Column<int>(nullable: false),
                    MandatoryPitstopCount = table.Column<int>(nullable: false),
                    MaxTotalDrivingTime = table.Column<int>(nullable: false),
                    MaxDriversCount = table.Column<int>(nullable: false),
                    RefuellingAllowedInRace = table.Column<bool>(nullable: false),
                    RefuellingTimeFixed = table.Column<bool>(nullable: false),
                    MandatoryPitstopRefuellingRequired = table.Column<bool>(nullable: false),
                    MandatoryPitstopTyreChangeRequired = table.Column<bool>(nullable: false),
                    MandatoryPitstopSwapDriverRequired = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    ServerName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    AdminPassword = table.Column<string>(nullable: true),
                    TrackMedalsRequirement = table.Column<int>(nullable: false),
                    SafetyRatingRequirement = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    RacecraftRatingRequirement = table.Column<int>(nullable: false),
                    SpectatorPassword = table.Column<string>(nullable: true),
                    AllowAutoDisqualification = table.Column<bool>(nullable: false),
                    RandomizeTrackWhenEmpty = table.Column<bool>(nullable: false),
                    ShortFormationLap = table.Column<bool>(nullable: false),
                    IsRaceLocked = table.Column<bool>(nullable: false),
                    DumpEntryList = table.Column<bool>(nullable: false),
                    DumpLeaderboards = table.Column<bool>(nullable: false),
                    MaxCarSlots = table.Column<int>(nullable: false),
                    CentralEntryListPath = table.Column<string>(nullable: true)
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
                    Id = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    UdpPort = table.Column<int>(nullable: false),
                    TcpPort = table.Column<int>(nullable: false),
                    MaxConnections = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    RegisterToLobby = table.Column<bool>(nullable: false)
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
                name: "SessionConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventCfgId = table.Column<Guid>(nullable: false),
                    HourOfDay = table.Column<int>(nullable: false),
                    DayOfWeekend = table.Column<int>(nullable: false),
                    TimeMultiplier = table.Column<int>(nullable: false),
                    SessionType = table.Column<int>(nullable: false),
                    SessionDurationMinutes = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_NetworkCfgs_ServerId",
                table: "NetworkCfgs",
                column: "ServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionConfiguration_EventCfgId",
                table: "SessionConfiguration",
                column: "EventCfgId");
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
                name: "EventRules");

            migrationBuilder.DropTable(
                name: "GameCfgs");

            migrationBuilder.DropTable(
                name: "NetworkCfgs");

            migrationBuilder.DropTable(
                name: "SessionConfiguration");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventCfgs");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
