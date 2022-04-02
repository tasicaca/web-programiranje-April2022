using Microsoft.EntityFrameworkCore;
using Template.Models;

namespace Template.Models
{
    public class IspitDbContext : DbContext
    {
        public DbSet<Prodavnica> Prodavnica{get;set;}
        public DbSet<Sara> Sara{get;set;}
        public DbSet<Ploca> Ploca {get;set;}
// DbSet...

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
