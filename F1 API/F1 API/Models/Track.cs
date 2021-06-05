using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace F1_API.Models
{
    public class Track
    {
       public int TrackId { get; set; }
       public string Country { get; set; }
       public int Laps { get; set; }
       public int Corners { get; set; }
       [Range(307,310)]
       public double RaceDistance { get; set; }
       public string TrackName { get; set; }
       public double TrackLength { get; set; }
       [JsonIgnore]
       public List<DriverWinsTrack> WinningDrivers { get; set; }
    }
}
