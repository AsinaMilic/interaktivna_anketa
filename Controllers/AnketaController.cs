using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace nesto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnketaController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public AnketaController(FakultetContext context)
        {
            Context=context;
        }

        [Route("DodatiAnketu")]
        [HttpPost]

        public async Task<ActionResult> DodajAnketu([FromBody] Anketa anketa)
        {
            if(anketa.Entitet > 3)
                return BadRequest("Los tip ankete!");
                
            if(anketa.ID<0)
                return BadRequest("id");
            //itd

            try
            {
                Context.Ankete.Add(anketa);
                await Context.SaveChangesAsync();
                
                return Ok($"Anketa je dodata! ID je :{anketa.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       

        [Route("PreuzmiAnketu")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiAtributeAnkete(int Anketaid)
        {
            if(Anketaid<0) return BadRequest("id");
            try{
                var anketa = await Context.Ankete.Where( a=> a.ID == Anketaid )
                                        .Select(ank=>new{
                                            Naziv=ank.Naziv,
                                            Info=ank.Info,
                                            Link=ank.Link,
                                            Telefon=ank.Telefon,
                                            Mail=ank.Mail
                                        }).FirstOrDefaultAsync();
                if(anketa==null)
                    return BadRequest("nismo izvukli anetu");
                else
                    return Ok(anketa);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("PromeniAnketu/{Anketaid}/{info}/{link}/{mail}/{naziv}/{telefon}")]
        [HttpPut]
        public async Task<ActionResult> PromeniAnketu(int Anketaid,string info,string link,string mail,string naziv,string telefon)
        {
            //provere
            try
            {
                var anketa= await Context.Ankete.Where(ank=>ank.ID==Anketaid).FirstOrDefaultAsync();

                if(anketa==null)
                    return BadRequest("nismo nasli anketu");
                
                anketa.Info=info;
                anketa.Link=link;
                anketa.Mail=mail;
                anketa.Naziv=naziv;
                anketa.Telefon=telefon;
                await Context.SaveChangesAsync();
                
                return Ok("promenili smo anketu");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
        
}