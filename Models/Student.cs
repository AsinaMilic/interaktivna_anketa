using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{    
    [Table("Student")] //ovako se zova tabela u bazi
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Ime {get; set;}

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Prezime{get; set;}

        [Required]
        [MinLength(5)]
        [MaxLength(100)]

        public string Mail{get; set;}

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Sifra{get; set;}

        public List<Popunjavanje> StudentPopunjavanje {get; set;}

        public List<Odgovor> StudentOdgovori {get; set;}
    }
}