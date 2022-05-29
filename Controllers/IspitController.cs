using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;////dodato za toListAsync
using Microsoft.Extensions.Logging;
using Template.Models;

namespace Template.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IspitController : ControllerBase
    {
        IspitDbContext Context { get; set; }

        public IspitController(IspitDbContext context)
        {
            Context = context;
        }

        [Route("PreuzmiProdavnice")]
        [HttpGet]
        public async Task<List<Prodavnica>> PreuzmiProdavnice()
        {
          //var vraceneProdavnice=await Context.Prodavnica.FindAsync(idProdavnice);
          return await Context.Prodavnica.ToListAsync();
        }


        [Route("PreuzmiSare/{idProdavnice}")]
        [HttpGet]
        public async Task<List<Sara>> PreuzmiSare(int idProdavnice)
        {
          var vraceneProdavnice=await Context.Prodavnica.FindAsync(idProdavnice);
          return await Context.Sara.Include(p=>p.Prodavnica).Where(p=>p.Prodavnica.Contains(vraceneProdavnice)).ToListAsync();
        }

        [Route("IzmenaPodatakaOPloci/{duzina}/{sirina}/{idSare}/{idProd}")]
        [HttpPut]
        public async Task<ActionResult> IzmenaPodatakaOPloci(float duzina, float sirina, int idSare,int idProd)
        {
          var nadjenaPloca = await Context.Ploca.Where(p=>p.Prodavnica.ID==idProd && p.Sara.ID==idSare && p.Otpadna==true  && p.Duzina>=duzina  && p.Sirina>=sirina && p.Brojnost>0).FirstOrDefaultAsync(); ///moras tamo gde je await da imas i async
          if (nadjenaPloca!=null){
              //nadjenaPloca.Duzina = nadjenaPloca.Duzina-duzina;
              nadjenaPloca.Sirina = nadjenaPloca.Sirina-sirina;

              if ((nadjenaPloca.Sirina<=0) || (nadjenaPloca.Duzina*nadjenaPloca.Sirina<=0.2*10*10)) 
                Context.Ploca.Remove(nadjenaPloca);

              else Context.Ploca.Update(nadjenaPloca);

              await Context.SaveChangesAsync();
              return Ok("IzmenaUspesna");
          //ukoliko moze koristi otpadnu plocu koja ima dovoljne dimenzije
          }
          
          else  
          {//ako je ploca iz te prodavnice i sa odgovarajucom sarom, ali nije otpadna, onda se pravi nova otpadna
              nadjenaPloca = await Context.Ploca.Where(p=>p.Prodavnica.ID==idProd && p.Sara.ID==idSare && p.Otpadna==false && p.Duzina>=duzina && p.Sirina>=sirina && p.Brojnost>0).FirstOrDefaultAsync();
              if (nadjenaPloca!= null){
        
              nadjenaPloca.Brojnost--;
              if (nadjenaPloca.Brojnost>0)
                Context.Ploca.Update(nadjenaPloca);
                else Context.Ploca.Remove(nadjenaPloca);

              if (nadjenaPloca.Duzina*(nadjenaPloca.Sirina-sirina)>0.2*10*10) {//10 je dimenzija 
                Ploca NovaPloca= new Ploca();
                NovaPloca.Brojnost=1;
                NovaPloca.Duzina=nadjenaPloca.Duzina;
                NovaPloca.Sirina=nadjenaPloca.Sirina-sirina;
                NovaPloca.Otpadna=true;

                var Prod=await Context.Prodavnica.Where(p=>p.ID==idProd).FirstOrDefaultAsync();//ono sto si pogresio je to da moras u Contextu naci konkretnu Prodavnicu da bi je dodelio kasnije (Ploca.Prodavnica)
                var Shara=await Context.Sara.Where(p=>p.ID==idSare).FirstOrDefaultAsync();
                NovaPloca.Prodavnica=Prod;
                NovaPloca.Sara=Shara;
              
                Context.Ploca.Add(NovaPloca);
              }
             
              await Context.SaveChangesAsync();
              return Ok("IzmenaUspesna");
              }
          else 
          {
            return BadRequest("Neuspesna Kupovina jer nema odgovarajuce ploce");
          }
        }
    }
}
}
//

/*Jedino sto smo dodali u ovom obrascu je ,
  "ConnectionStrings": {
    "IspitCS": "Server=(localdb)\\MSSQLLocalDB;Database=TestBazaPodataka"   
  },
  "AllowedHosts": "*" i index.html */