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
    public class PitanjeController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public PitanjeController(FakultetContext context)
        {
            Context=context;
        }
        [Route("PreuzmiPitanja/{Entitet}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPitanja(int Entitet)
        {
            if(Entitet<0) return BadRequest("los entitet");
            
            try
            {   
                var pitanja = await Context.Pitanja.Where(p=>p.anketa.Entitet == Entitet)
                    .Select( p=>new{
                                    p.ID,
                                    p.tip_pitanja,
                                    p.Tekst_pitanja,
                                    p.Moguci_odgovori
                                }
                            ).ToListAsync();                
    
                if(pitanja == null)
                    return BadRequest("pitanja ne postoje");
                return Ok(pitanja);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodatiPitanje/{tekst}/{anketaID}/{tip}/{odgovori}")]
        [HttpPost]
        public async Task<ActionResult> DodajPitanje(string tekst,int anketaID,int tip,string odgovori)
        {

            if(anketaID<0) return BadRequest("id");

            try
            {
                var anketica=await Context.Ankete.Where(ank=>ank.ID==anketaID).FirstOrDefaultAsync();
                
                Pitanje pitanje=new Pitanje
                {
                    Tekst_pitanja=tekst,
                    tip_pitanja=tip,
                    Moguci_odgovori=odgovori,
                    anketa=anketica
                };

                Context.Pitanja.Add(pitanje);
                await Context.SaveChangesAsync();
                return Ok($"Pitanje je dodato!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenitiPitanje/{IDpitanja}/{tekst}/{odgovori}")]
        [HttpPut]
        public async Task<ActionResult> PromenitiPitanje(int IDpitanja,string tekst,string odgovori)
        {

            if(IDpitanja<0) return BadRequest("los id pitanja");

            try
            {
                var pitanjce=await Context.Pitanja.Where(pit=>pit.ID==IDpitanja).FirstOrDefaultAsync();
                
                if(pitanjce!=null)
                {
                    pitanjce.Tekst_pitanja=tekst;
                    pitanjce.Moguci_odgovori=odgovori;
                    await Context.SaveChangesAsync();
                    return Ok("Uspesno smo dodali pitanje wow");
                }
                else
                {
                    return BadRequest("nismo pronasli pitanje");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiPitanje/{IDpitanja}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiPitanje(int IDpitanja)
        {
            try
            {
                var pitanjce=await Context.Pitanja.Where(pit=>pit.ID==IDpitanja).FirstOrDefaultAsync();

                    Context.Pitanja.Remove(pitanjce);
                    await Context.SaveChangesAsync();
                    return Ok("Obrisao si pitanje");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}