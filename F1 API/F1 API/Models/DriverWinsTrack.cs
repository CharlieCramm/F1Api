using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace F1_API.Models
{
    public class DriverWinsTrack
    {
        
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int TrackId { get; set; }
        [JsonIgnore]
        public Track Track { get; set; }
    }
}
