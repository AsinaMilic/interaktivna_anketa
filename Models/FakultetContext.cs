using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class FakultetContext : DbContext
    {
        public DbSet<Student> Studenti {get; set;}

        public DbSet<Popunjavanje> Popunjavanja {get; set;}

        public DbSet<Anketa> Ankete {get; set;}

        public DbSet<Odgovor> Odgovori {get; set;}
        
        public DbSet<Administrator> Administratori {get; set;}
       
        public DbSet<Fakultet> Fakulteti {get; set;}

        public DbSet<Pitanje> Pitanja {get; set;}

        public FakultetContext(DbContextOptions options) : base(options)
        {

        }
    }
}