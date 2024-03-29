﻿// <auto-generated />
using System;
using F1_API.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace F1_API.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20210525104709_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("F1_API.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Drivernumber")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("F1_API.Models.DriverWinsTrack", b =>
                {
                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("DriverId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("driverWinsTracks");
                });

            modelBuilder.Entity("F1_API.Models.Engine", b =>
                {
                    b.Property<int>("EngineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BHP")
                        .HasColumnType("float");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("EngineName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EngineId");

                    b.ToTable("engines");
                });

            modelBuilder.Entity("F1_API.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EngineId")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamNationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.HasIndex("EngineId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("F1_API.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Corners")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Laps")
                        .HasColumnType("int");

                    b.Property<double>("RaceDistance")
                        .HasColumnType("float");

                    b.Property<double>("TrackLength")
                        .HasColumnType("float");

                    b.Property<string>("TrackName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrackId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("F1_API.Models.Driver", b =>
                {
                    b.HasOne("F1_API.Models.Team", "Team")
                        .WithMany("Drivers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("F1_API.Models.DriverWinsTrack", b =>
                {
                    b.HasOne("F1_API.Models.Driver", "Driver")
                        .WithMany("TrackWins")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("F1_API.Models.Track", "Track")
                        .WithMany("WinningDrivers")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("F1_API.Models.Team", b =>
                {
                    b.HasOne("F1_API.Models.Engine", "Engine")
                        .WithMany("Teams")
                        .HasForeignKey("EngineId");

                    b.Navigation("Engine");
                });

            modelBuilder.Entity("F1_API.Models.Driver", b =>
                {
                    b.Navigation("TrackWins");
                });

            modelBuilder.Entity("F1_API.Models.Engine", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("F1_API.Models.Team", b =>
                {
                    b.Navigation("Drivers");
                });

            modelBuilder.Entity("F1_API.Models.Track", b =>
                {
                    b.Navigation("WinningDrivers");
                });
#pragma warning restore 612, 618
        }
    }
}
