using System;
using System.Collections.Generic;
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
    public class OdgovorController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public OdgovorController(FakultetContext context)
        {
            Context=context;
        
        }

        [Route("DodajVratiOdgovore")]
        [HttpPost]   //update-ujem 2 tabele     //1.3.7 ne radi ako je samo jedna brojka string
        public async Task<ActionResult> DodajVratiOdgovoreString(string pitanjaID,string tekst_odgovora, string komentarODG
                                                      ,int anketaId,int studentID)
        {
                if(string.IsNullOrWhiteSpace(pitanjaID)) return BadRequest("losa pitanjaID");
                if(anketaId<0) return BadRequest("AnketaId");
                if(studentID<0) return BadRequest("studentID");
                
                int[] pitanjaid=Array.ConvertAll(pitanjaID.Split(','),int.Parse);
                string[] tekstodgovora=tekst_odgovora.Split(',');
                string[] komentarodg=komentarODG.Split(',');
            try
            {
                
                var Popunjavanje = await Context.Popunjavanja.Where(p=>p.anketa.ID==anketaId && p.student.ID==studentID).FirstOrDefaultAsync();
                var Student=await Context.Studenti.Where(s=>s.ID==studentID).FirstOrDefaultAsync();

                Popunjavanje.Popunjena=true;
                
                List<dynamic> list = new List<dynamic>();        

                for(int i=0;i<pitanjaid.Length;i++)
                {
                    if(tekstodgovora[i]=="NULL") tekstodgovora[i]=null;
                    if(komentarodg[i]=="NULL") komentarodg[i]=null;
                    var odgovor=new Odgovor
                    {
                        Tekst_odgovora=tekstodgovora[i],
                        Komentar=komentarodg[i],
                        student=Student,
                        pitanje=await Context.Pitanja.Where(p=>p.ID==pitanjaid[i]).FirstOrDefaultAsync()
                        
                    };
            
                    Context.Odgovori.Add(odgovor);
                }
                await Context.SaveChangesAsync();

                return Ok("Dodali smo odogovore"); 
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }   

        [Route("VratiStudenteOdgovora")]
        [HttpGet]

        public async Task<ActionResult> VratiStudenteOdgovora(string pitanjaID)
        {
            if(string.IsNullOrWhiteSpace(pitanjaID)) return BadRequest("pitanjaID");
            
            int[] pitanjaid=Array.ConvertAll(pitanjaID.Split(','),int.Parse);
            int Length=pitanjaid.Length;
            List<dynamic> list = new List<dynamic>();
            try
            {
                for(int i=0;i<Length;i++)
                {
                    var ImePrezime = await Context.Odgovori.Where(odg=> odg.pitanje.ID == pitanjaid[i] )
                                                            .Select(odgovor => new
                                                            {
                                                                tekst_odgovora=odgovor.Tekst_odgovora,
                                                                komentar=odgovor.Komentar,
                                                                Ime =odgovor.student.Ime,
                                                                Prezime= odgovor.student.Prezime
                                                            })
                                                            .ToListAsync();
                    list.Add(ImePrezime);
                }

                if(list==null)
                    return BadRequest("Nismo nasli studente za odgovore");
                else
                    return Ok(list);

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }   

}