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
    public class AdministratorController : ControllerBase
    {
        public FakultetContext Context{get; set;}
        public AdministratorController(FakultetContext context)
        {
            Context=context;
        }

        [Route("VratitiAdministratora")]
        [HttpGet]
        public async Task<ActionResult> VratiAdministratora(string username,string sifra)
        {
            if(string.IsNullOrWhiteSpace(username)||string.IsNullOrWhiteSpace(sifra))
                return BadRequest("nullstring");

            try
            {
                var admin = await Context.Administratori.Where(admin=> admin.Korisnicko_ime==username && admin.Sifra==sifra)
                                                        .Select(adm=>new
                                                        {
                                                            Korisnicko_ime=adm.Korisnicko_ime,
                                                            Sifra=adm.Sifra
                                                        }).FirstOrDefaultAsync();

                if(admin==null)
                    return BadRequest("Greska u pronalasku admina");
                else
                    return Ok(admin);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("DodatiAdministratora")]
        [HttpPost]
        public async Task<ActionResult> DodajAdministratora([FromBody] Administrator administrator)
        {
            if(string.IsNullOrEmpty(administrator.Korisnicko_ime) || administrator.Korisnicko_ime.Length > 50)
                return BadRequest("Lose ime!");

            if(string.IsNullOrEmpty(administrator.Sifra) || administrator.Sifra.Length > 50)
                return BadRequest("Losa sifra!");

            try
            {
                Context.Administratori.Add(administrator);
                await Context.SaveChangesAsync();

                return Ok($"Admin je dodat! ID je: {administrator.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenitiAdministratora/{korisnicko_ime}/{sifra}")]
        [HttpPut]
        public async Task<ActionResult> Promeni(string korisnicko_ime,string sifra)
        {
            if(string.IsNullOrEmpty(korisnicko_ime) || korisnicko_ime.Length > 50)
                return BadRequest("Lose korisnicko ime!");
            
            if(string.IsNullOrEmpty(sifra) || sifra.Length > 50)
                return BadRequest("Losa sifra!");
            
            try
            {
                var admin= Context.Administratori. Where(p => p.Korisnicko_ime == korisnicko_ime && p.Sifra == sifra).FirstOrDefault();

                if(admin!=null)
                {
                    admin.Korisnicko_ime = korisnicko_ime;
                    admin.Sifra = sifra;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno promenjen administrator! ID: {admin.ID}");
                }
                else
                    return BadRequest("Administrator nije pronadjen!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}