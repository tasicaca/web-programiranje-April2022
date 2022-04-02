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

        [Route("IzmenaPodatakaOPloci/{duzina}/{sirina}/{idSare}/{idPloce}")]
        [HttpPut]
        public async Task IzmenaPodatakaOPloci(float duzina, float sirina, int idSare,int idPloce)
        {
          var nadjenaPloca = await Context.Ploca.Where(p=>p.Prodavnica.ID==idPloce && p.Sara.ID==idSare).FirstOrDefaultAsync(); ///moras tamo gde je await da imas i async
          nadjenaPloca.Duzina= nadjenaPloca.Duzina-duzina;
          nadjenaPloca.Sirina=nadjenaPloca.Sirina-sirina;
          nadjenaPloca.Otpadna=true;
          await Context.SaveChangesAsync();
        }
    }
}


/*Jedino sto smo dodali je ,
  "ConnectionStrings": {
    "IspitCS": "Server=(localdb)\\MSSQLLocalDB;Database=TestBazaPodataka"   
  },
  "AllowedHosts": "*" i index.html */