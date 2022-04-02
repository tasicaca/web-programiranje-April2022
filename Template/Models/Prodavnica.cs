using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models
{
    [Table("Prodavnica")]
    public class Prodavnica
    {
        [Key] 
        public int ID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Neophodno je uneti naziv !")]
       
        public string Naziv { get; set; }

        public List<Ploca> Ploca {get;set;}

        [JsonIgnore]
        public List<Sara> Sara {get;set;}
        

        public Prodavnica()
        {
            this.Ploca = new List<Ploca>();
            this.Sara = new List<Sara>();
        }
    }
}
