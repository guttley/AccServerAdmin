﻿// <auto-generated />
using System;
using AccServerAdmin.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AccServerAdmin.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.BalanceOfPerformance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ballast")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Car")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Restrictor")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Track")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("BalanceOfPerformance");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AiAggro")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AiConsistency")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AiRainSkill")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AiSkill")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DriverCategory")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<int>("GlovesTemplateKey")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HelmetBaseColor")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HelmetDetailColor")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HelmetGlassColor")
                        .HasColumnType("INTEGER");

                    b.Property<double>("HelmetGlassMetallic")
                        .HasColumnType("REAL");

                    b.Property<int>("HelmetMaterialType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HelmetTemplateKey")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Shortname")
                        .HasColumnType("TEXT");

                    b.Property<int>("SuitDetailColor1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SuitDetailColor2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SuitTemplateKey")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.DriverEntry", b =>
                {
                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("TEXT");

                    b.HasKey("EntryId", "DriverId");

                    b.HasIndex("DriverId");

                    b.ToTable("DriverEntries");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.Entry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ballast")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigVersion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomCar")
                        .HasColumnType("TEXT");

                    b.Property<int>("DefaultGridPosition")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("EntryListId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForcedCarModel")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OverrideCarModelForCustomCar")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OverrideDriverInfo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RaceNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Restrictor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ServerAdmin")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntryListId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.EntryList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigVersion")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ForceEntryList")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServerId")
                        .IsUnique();

                    b.ToTable("EntryList");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.EventCfg", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AmbientTemp")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CloudLevel")
                        .HasColumnType("REAL");

                    b.Property<string>("EventType")
                        .HasColumnType("TEXT");

                    b.Property<int>("PostQualySeconds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PostRaceSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PreRaceWaitingTimeSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rain")
                        .HasColumnType("REAL");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SessionOverTimeSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Track")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeatherRandomness")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ServerId")
                        .IsUnique();

                    b.ToTable("EventCfgs");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.EventRules", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("DriverStintTime")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MandatoryPitstopCount")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MandatoryPitstopRefuellingRequired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MandatoryPitstopSwapDriverRequired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MandatoryPitstopTyreChangeRequired")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxDriversCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxTotalDrivingTime")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PitWindowLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QualifyType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RefuellingAllowedInRace")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RefuellingTimeFixed")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServerId")
                        .IsUnique();

                    b.ToTable("EventRules");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.GameCfg", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminPassword")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AllowAutoDisqualification")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CentralEntryListPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DumpEntryList")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DumpLeaderboards")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FormationLap")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRaceLocked")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxCarSlots")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("RacecraftRatingRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RandomizeTrackWhenEmpty")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SafetyRatingRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServerName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ShortFormationLap")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpectatorPassword")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrackMedalsRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ServerId")
                        .IsUnique();

                    b.ToTable("GameCfgs");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.NetworkCfg", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxConnections")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RegisterToLobby")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TcpPort")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UdpPort")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ServerId")
                        .IsUnique();

                    b.ToTable("NetworkCfgs");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.SessionConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("DayOfWeekend")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("EventCfgId")
                        .HasColumnType("TEXT");

                    b.Property<int>("HourOfDay")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionDurationMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeMultiplier")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventCfgId");

                    b.ToTable("SessionConfiguration");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AppSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminPassphrase")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InstanceBasePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ServerBasePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppSettings");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.BalanceOfPerformance", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.Server", null)
                        .WithMany("ServerBop")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.Driver", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.AccConfig.Entry", null)
                        .WithMany("Drivers")
                        .HasForeignKey("EntryId");
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.DriverEntry", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.AccConfig.Driver", "Driver")
                        .WithMany("Entries")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccServerAdmin.Domain.AccConfig.Entry", "Entry")
                        .WithMany("Entries")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.Entry", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.AccConfig.EntryList", null)
                        .WithMany("Entries")
                        .HasForeignKey("EntryListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.EntryList", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.Server", null)
                        .WithOne("EntryList")
                        .HasForeignKey("AccServerAdmin.Domain.AccConfig.EntryList", "ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.EventCfg", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.Server", null)
                        .WithOne("EventCfg")
                        .HasForeignKey("AccServerAdmin.Domain.AccConfig.EventCfg", "ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.EventRules", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.Server", null)
                        .WithOne("EventRules")
                        .HasForeignKey("AccServerAdmin.Domain.AccConfig.EventRules", "ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.GameCfg", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.Server", null)
                        .WithOne("GameCfg")
                        .HasForeignKey("AccServerAdmin.Domain.AccConfig.GameCfg", "ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.NetworkCfg", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.Server", null)
                        .WithOne("NetworkCfg")
                        .HasForeignKey("AccServerAdmin.Domain.AccConfig.NetworkCfg", "ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccServerAdmin.Domain.AccConfig.SessionConfiguration", b =>
                {
                    b.HasOne("AccServerAdmin.Domain.AccConfig.EventCfg", null)
                        .WithMany("Sessions")
                        .HasForeignKey("EventCfgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
