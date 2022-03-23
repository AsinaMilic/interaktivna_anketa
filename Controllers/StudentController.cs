using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace nesto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public StudentController(FakultetContext context)
        {
            Context=context;
        
        }
        

        [Route("PreuzmiStudenta/{StudentMail}/{StudentPassword}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiStudenta(string StudentMail, string StudentPassword)
        {
            try{
                var student=await Context.Studenti.Where(x=>x.Mail==StudentMail && x.Sifra==StudentPassword)
                    .Select(s=> new {
                        s.ID,
                        s.Ime,
                        s.Prezime
                    }).FirstOrDefaultAsync();
                
                
                if (student==null)
                    return BadRequest("Student ne postoji!");

                return Ok(student);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodatiStudenta")]
        [HttpPost]
        public async Task<ActionResult> DodajStudenta([FromBody] Student student)
        {

            if (string.IsNullOrWhiteSpace(student.Ime) || student.Ime.Length > 50)
                return BadRequest("Pogrešno ime!");

            if (string.IsNullOrWhiteSpace(student.Prezime) || student.Prezime.Length > 50)
                return BadRequest("Pogrešno prezime!");

            try
            {
                var popunjavanja= await Context.Ankete
                    .Select(a=>
                        new Popunjavanje 
                        {
                            Popunjena=false,               
                            student=student,
                            anketa=a
                        }
                    ).ToListAsync();
                
                Context.Studenti.Add(student);
                popunjavanja.ForEach(popunjeno=>Context.Popunjavanja.Add(popunjeno)); //radi, daje popunjavanja
                
                await Context.SaveChangesAsync(); //posto je asihrono, ovo cuvanje ce se izvrsavati u novoj niti, a ostatak kontrolera ce moci da se izvrsava
                                                  //mada ce se zaustaviti na toj liniji sve dok ne dobije odgovor od servera, await kljucna rec
                return Ok($"Student je dodat! ID je: {student.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
       
    }
}
