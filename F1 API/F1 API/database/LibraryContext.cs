using F1_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_API.database
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Engine> engines { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<DriverWinsTrack> driverWinsTracks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DriverWinsTrack>()
                .HasKey(x => new { x.DriverId, x.TrackId });
            modelBuilder.Entity<DriverWinsTrack>()
                .HasOne(x => x.Driver)
                .WithMany(x => x.TrackWins)
                .HasForeignKey(x => x.DriverId);
            modelBuilder.Entity<DriverWinsTrack>()
                .HasOne(x => x.Track)
                .WithMany(x => x.WinningDrivers)
                .HasForeignKey(x => x.TrackId);
            modelBuilder.Entity<Team>().HasMany(x => x.Drivers)
                .WithOne(x => x.Team);
            modelBuilder.Entity<Team>().HasOne(x => x.Engine)
                .WithMany(x => x.Teams);
        }
    }
}
