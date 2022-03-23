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
    public class PopunjavanjeController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public PopunjavanjeController(FakultetContext context)
        {
            Context=context;
        
        }

        [Route("PreuzmiNePopunjeneAnkete/{idStudenta}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiNePopunjeneAnkete(int idStudenta)
        {
            if(idStudenta<0) return BadRequest("los id");
            try{                                                                                        
                var nepopunjene=await Context.Popunjavanja.Where(p=>p.student.ID==idStudenta && p.Popunjena==false)
                    .Select(p=> new {
                        p.anketa.ID, 
                        p.anketa.Entitet
                        }).ToListAsync();   
                
                if (nepopunjene==null)
                    return BadRequest("Ne postoji veza studenta sa datom anketom!");
                
                return Ok(nepopunjene);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DodajPopunjenost/{idStudenta}/{idAnkete}")]
        [HttpPost]
        public async Task<ActionResult> DodajPopunjenost(int idStudenta, int idAnkete) //potencijalno komentar
        {
            if(idStudenta<0 || idAnkete<0) return BadRequest("losi id-evi");
            try
            {
                var studentic= await Context.Studenti.Where(s => s.ID == idStudenta).FirstOrDefaultAsync();
                var anketica = await Context.Ankete.Where(a => a.ID == idAnkete).FirstOrDefaultAsync();

                Popunjavanje pop = new Popunjavanje
                {
                    Popunjena=false,
                    anketa = anketica,
                    student = studentic,
                };

                Context.Popunjavanja.Add(pop);
                await Context.SaveChangesAsync();
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
    }
}
