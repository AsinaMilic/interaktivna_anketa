using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Fakultet")]
    public class Fakultet
    {
        [Key]
        public int ID {get; set;}

        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Naziv {get; set;}

        [MaxLength(666)]
        public string Info {get; set;}

        public Administrator administrator {get; set;}
    
        public List<Anketa> FakultetAnkete {get; set;}
    }

}