using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Odgovor")]
    public class Odgovor
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(1000)]
        public string Tekst_odgovora {get; set;}
        [MaxLength(666)]
        public string Komentar{get; set;}
        
        public Pitanje pitanje{get; set;}

        public Student student{get; set;}
       // public Popunjavanje popunjavanje{get; set;}

    }
}