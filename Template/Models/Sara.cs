using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models{
    [Table("Sara")]
    public class Sara
    {
        [Key]
        public int ID{get;set;}

        public string Naziv{get;set;}

        [JsonIgnore]//sluzi da spreci serijalizaciju u json tip
        public List<Prodavnica> Prodavnica{get;set;}
     
        public List<Ploca> Ploca{get;set;}
        public Sara()
        {
           this.Prodavnica=new List<Prodavnica>();
           this.Ploca=new List<Ploca>();
        }
    }
}
