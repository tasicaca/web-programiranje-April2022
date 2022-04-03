using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Template.Models{
    [Table("Ploca")]
    public class Ploca
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [Column("Brojnost")]////tu treba da izmenis brojnost ploca i da stavis da imas vise ploca, kao i da promenis ovo za naziv, ali generalno ti radi
        public int Brojnost{get;set;}
        
        [Column("Duzina")]
        public float Duzina{get;set;}

        [Column("Sirina")]
        public float Sirina{get;set;}

        [Column("Otpadna")]
        public bool Otpadna{get;set;}

        public virtual Prodavnica Prodavnica{get;set;}

        public virtual Sara Sara{get;set;}

        public Ploca()
        {
        }
    }
}