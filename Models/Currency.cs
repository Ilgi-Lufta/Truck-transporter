#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Currency
{
    [Key]
    public int CurrencyId { get; set; }
    public string CurrencyUnit { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public List<RrugaShpenzimeEkstra> RrugaShpenzimeEkstras { get; set; } = new List<RrugaShpenzimeEkstra>();
    public List<RrugaFitimeEkstra> RrugaFitimeEkstras { get; set; } = new List<RrugaFitimeEkstra>();
    public List<RrugaFitime> RrugaFitimes { get; set; } = new List<RrugaFitime>();
    public List<PagesaShoferit> PagesaShoferits { get; set; } = new List<PagesaShoferit>();
    public List<PagesaPikaShkarkimit> PagesaPikaShkarkimits { get; set; } = new List<PagesaPikaShkarkimit>();
    public List<PagesaDogana> PagesaDoganas { get; set; } = new List<PagesaDogana>();
    public List<PagesaNafta> PagesaNaftas { get; set; } = new List<PagesaNafta>();

   // public List<PikaRruga> PikaRrugas { get; set; } = new List<PikaRruga>();
   // public List<ShoferRruga> shoferRrugas { get; set; } = new List<ShoferRruga>();
   // public List<NaftaRruga> NaftaRrugas { get; set; } = new List<NaftaRruga>();







}
