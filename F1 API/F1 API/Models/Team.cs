using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace F1_API.Models
{
    public class Team
    {
        
        public int TeamId { get; set; }

        
        [Required]
        public string TeamName { get; set; }

        [MaxLength(30)]
        [Required]
        public string TeamNationality { get; set; }

        public Engine Engine { get; set; }
        [JsonIgnore]
        public List<Driver> Drivers { get; set; }
        //public object Drivers { get; internal set; }
    }
}
