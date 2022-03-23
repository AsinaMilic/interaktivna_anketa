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
    public class FakultetController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public FakultetController(FakultetContext context)
        {
            Context=context;
        
        }
        

        [Route("PreuzmiFakultete")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiFakultete()
        {
            try
            {
                var fakulteti=await Context.Fakulteti.Select(faks=>new{
                                                                id=faks.ID,
                                                                Naziv=faks.Naziv,
                                                                Info=faks.Info
                                                            }).ToListAsync();
                if(fakulteti==null)
                     return BadRequest("Faks greska");
                else
                    return Ok(fakulteti);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
        }
        [Route("PreuzmiFakultet/{faksID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiFakultet(int faksID)
        {
            
            if(faksID<0)return BadRequest("id");
            try
            {
                var fakultet=await Context.Fakulteti.Where(f=>f.ID==faksID)
                                                    .Select(faks=>new{
                                                                Naziv=faks.Naziv,
                                                                Info=faks.Info
                                                            }).FirstOrDefaultAsync();
                if(fakultet==null)
                     return BadRequest("Faks greska");
                else
                    return Ok(fakultet);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("PreuzmiAnketeZaFaks")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiAnketeZaFaks(string naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv)) return BadRequest("nullstring");

            try
            {
                var query =await Context.Fakulteti.Where(f=>f.Naziv==naziv)
                                                   .Include(f=>f.FakultetAnkete)
                                                   .ToListAsync();
                
                List<dynamic> ankete= new List<dynamic>();
                
                foreach(var faks in query)
                {
                    foreach(var anketa in faks.FakultetAnkete)
                    {
                        var obj=new {
                            id=anketa.ID,
                            entitet=anketa.Entitet
                        };
                        ankete.Add(obj);
                    }
                }                    
                                                    

                if(ankete==null)
                     return BadRequest("Faks greska");
                else
                    return Ok(ankete);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
        }
        [Route("PromenitiFakultet")]
        [HttpPut]
        public async Task<ActionResult> PromeniteFakultet(int faksID,string naziv,string info)
        {
            if(faksID<0)return BadRequest("id");

            try
            {
                var faks=await Context.Fakulteti.Where(f=>f.ID==faksID).FirstOrDefaultAsync();
                
                if(faks==null)
                    return BadRequest("nije nadjen");
                
                if(!string.IsNullOrWhiteSpace(naziv))
                    faks.Naziv=naziv;
                
                if(!string.IsNullOrWhiteSpace(info))       
                    faks.Info=info;
                
                await Context.SaveChangesAsync();
                return Ok("promenjeno!");
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}