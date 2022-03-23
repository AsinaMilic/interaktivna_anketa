using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Popunjavanje")]
    public class Popunjavanje  //bukvalno Spoj
    {
        [Key]
        public int ID { get; set; }

        public bool Popunjena {get; set;}

        public Anketa anketa {get;set;}

        public Student student{get; set;}

    }
}