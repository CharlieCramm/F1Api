using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace F1_API.Models
{
    public class Engine
    {
        [Key]
        public int EngineId { get; set; }

        public Double BHP { get; set; }

        public Double Cost { get; set; }

        public string EngineName { get; set; }
        [JsonIgnore]
        public List<Team> Teams { get; set; }
    }
}
