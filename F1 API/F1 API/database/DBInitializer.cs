using F1_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_API.database
{
    public class DBInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            //Create the db if not yet exists
            context.Database.EnsureCreated();

            Driver max = null;
            Driver perez = null;
            Driver lewis = null;
            Driver valtteri = null;

            Team mercedes = null;
            Team redbull = null;

            Track portimao = null;
            Track mugello = null;
            Track imola = null;

            DriverWinsTrack win1 = null;
            DriverWinsTrack win2 = null;

            Engine hondaEngine = null;
            Engine mercedesEngine = null;

            if (!context.Teams.Any())
            {
                //Create new team
                redbull = new Team()
                {
                    TeamName = "Red Bull Racing",
                    TeamNationality = "Austrian",
                    Engine = hondaEngine
                };

                mercedes = new Team()
                {
                    TeamName = "Mercedes-AMG Petronas F1 Team",
                    TeamNationality = "German",
                    Engine = mercedesEngine
                };
                context.Teams.Add(redbull);
                context.Teams.Add(mercedes);
                //save changes to db
                context.SaveChanges();
            }

            if (!context.Drivers.Any())
            {
                //Create new driver
   
                max = new Driver()
                {
                    Drivernumber = 33,
                    FirstName = "Max",
                    LastName = "verstappen",
                    BirthDate = new DateTime(1997, 9, 30),
                    Nationality = "Dutch",
                    Team = redbull
                };
                perez = new Driver()
                {
                    Drivernumber = 11,
                    FirstName = "Sergio",
                    LastName = "perez",
                    BirthDate = new DateTime(1990, 1, 26),
                    Nationality = "Mexican",
                   Team = redbull
                };

                lewis = new Driver()
                {
                    Drivernumber = 44,
                    FirstName = "Lewis",
                    LastName = "Hamilton",
                    BirthDate = new DateTime(1985, 1, 7),
                    Nationality = "Brittish",
                    Team = mercedes
                };
                valtteri = new Driver()
                {
                    Drivernumber = 77,
                    FirstName = "Valtteri",
                    LastName = "Bottas",
                    BirthDate = new DateTime(1989, 8, 28),
                    Nationality = "Finnish",
                    Team = mercedes
                };
                //add drivers 
                context.Drivers.Add(lewis);
                context.Drivers.Add(valtteri);
                context.Drivers.Add(max);
                context.Drivers.Add(perez);
                //save changes to db
                context.SaveChanges();

            }
            
            if (!context.Tracks.Any())
            {
                portimao = new Track()
                {
                    Country = "Portugal",
                    Corners = 16,
                    TrackLength = 4.692,
                    TrackName = "Autódromo Internacional do Algarve",
                    Laps = 66,
                    RaceDistance = 309.672
                };
                mugello = new Track()
                {
                    Country = "Italy",
                    Corners = 14,
                    TrackLength = 5.245,
                    TrackName = "Autodromo Internazionale del Mugello",
                    Laps = 59,
                    RaceDistance = 309.455
                };
                imola = new Track()
                {
                    Country = "Italy",
                    Corners = 17,
                    TrackLength = 4.909,
                    TrackName = "Autodromo Enzo e Dino Ferrari",
                    Laps = 63,
                    RaceDistance = 309.267
                };
                //save changes to db
                context.Tracks.Add(portimao);
                context.Tracks.Add(mugello);
                context.Tracks.Add(imola);
                context.SaveChanges();
            }
            if (!context.driverWinsTracks.Any()) 
            {
                win1 = new DriverWinsTrack()
                {
                    Driver = lewis,
                    Track = portimao,
                    DriverId = lewis.Drivernumber,
                    TrackId = portimao.TrackId
                };
                win2= new DriverWinsTrack()
                {
                    Driver = max,
                    Track = imola,
                    DriverId = max.Drivernumber,
                    TrackId = imola.TrackId
                };
                context.driverWinsTracks.Add(win1);
                context.driverWinsTracks.Add(win2);
                context.SaveChanges();
            }
            if (!context.engines.Any())
            {
                hondaEngine = new Engine()
                {
                    EngineName = "Honda V6 turbocharged hybrid",
                    BHP = 740,
                    Cost = 3400000
                };
                mercedesEngine = new Engine()
                {
                   
                    EngineName = "Mercedes-AMG V6 turbocharged hybrid",
                    BHP = 765,
                    Cost = 4100000
                };
                context.engines.Add(hondaEngine);
                context.engines.Add(mercedesEngine);
                context.SaveChanges();
            }
         }
    }
}
