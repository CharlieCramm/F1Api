using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace F1_API.Models
{
    public class Driver
    {
       
        [Key] 
        public int Id { get; set; }
        [Required]
        public int Drivernumber { get; set; }

        [Required]
        public Team Team { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        
        [Required]
        public string LastName { get; set; }

        
        public DateTime BirthDate { get; set; }

        
        [Required]
        public string Nationality { get; set; }
        [JsonIgnore]
        public List<DriverWinsTrack> TrackWins { get; set; }


    }
}
