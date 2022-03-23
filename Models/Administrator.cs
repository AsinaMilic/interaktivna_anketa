using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        public int ID {get; set;}

        [Required]
        [MaxLength(99)]
        public string Korisnicko_ime {get; set;}
        
        [Required]
        [MaxLength(99)]
        public string Sifra {get; set;}
        
        public List<Fakultet> AdminFakultet {get; set;}
        public List<Anketa> AdminAneta{get;set;}
        
       
    }
}