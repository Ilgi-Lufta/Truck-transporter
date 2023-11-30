#pragma warning disable CS8618
/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
using Microsoft.EntityFrameworkCore;

namespace BioLab.Models;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }
    // the "Monsters" table name will come from the DbSet property name
    public DbSet<ZbritShtoGjendja> ZbritShtoGjendjas { get; set; }
    public DbSet<ShoferRruga> ShoferRrugas { get; set; }
    public DbSet<Shofer> Shofers { get; set; }
    public DbSet<RrugaShpenzimeEkstra> RrugaShpenzimeEkstras { get; set; }
    public DbSet<RrugaFitimeEkstra> RrugaFitimeEkstras { get; set; }
    public DbSet<RrugaFitime> RrugaFitimes { get; set; }
    public DbSet<Rruga> Rrugas { get; set; }
    public DbSet<PikaShkarkimi> PikaShkarkimis { get; set; }
    public DbSet<PikaRruga> PikaRrugas { get; set; }
    public DbSet<PagesaShoferit> PagesaShoferits { get; set; }
    public DbSet<PagesaPikaShkarkimit> PagesaPikaShkarkimits { get; set; }
    public DbSet<PagesaDogana> PagesaDoganas { get; set; }
    public DbSet<Nafta> Naftas { get; set; }
   // public DbSet<NaftaRruga> NaftaRrugas { get; set; }
    public DbSet<Currency> Currencys { get; set; }
    public DbSet<PikaRrugaPagesa> PikaRrugaPagesas { get; set; }
    public DbSet<BlereShitur> BlereShiturs { get; set; }
    public DbSet<NaftaStock> NaftaStocks { get; set; }


}