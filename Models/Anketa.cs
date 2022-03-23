using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Anketa")]
    public class Anketa
    {
        [Key]
        public int ID { get; set; }
        public int Entitet {get; set;}
        [MaxLength(99)]
        public string Naziv{get; set;}
        [MaxLength(99)]
        public string Info {get; set;}
        [MaxLength(99)]
        public string Link{get; set;}
        [MaxLength(99)]
        public string Telefon {get; set;}
        [MaxLength(99)]
        public string Mail{get; set;}
        
        public List<Popunjavanje> AnketaPopunjavanje {get; set;} 
        public List<Pitanje> AnketaPitanja {get; set;} 
        public List<Fakultet> AnketaFakulteti{get; set;}
        public List<Administrator> AnketaAdministrator{get; set;}
        
        

    }
}