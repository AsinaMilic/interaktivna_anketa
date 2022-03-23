using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Pitanje")] //ovako se zove tabela u azure studio NEKA SE ZOVE ISTO KAO KLASAAA
    public class Pitanje
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(666)]
        public string Tekst_pitanja {get; set;}   //tip ankete
        
        public int tip_pitanja {get; set;} //0-izaberi, 1-oceni, 2-napisi
        [MaxLength(666)]
        public string Moguci_odgovori {get; set;}
        
        public List<Odgovor> PitanjeOdgovori {get; set;}
        public Anketa anketa{get; set;}

    }
}