#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Currency
{
    [Key]
    public int CurrencyId { get; set; }
    [Display(Name = "Monedha")]
    public string CurrencyUnit { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public List<RrugaShpenzimeEkstra> RrugaShpenzimeEkstras { get; set; } = new List<RrugaShpenzimeEkstra>();
    public List<RrugaFitimeEkstra> RrugaFitimeEkstras { get; set; } = new List<RrugaFitimeEkstra>();
    public List<RrugaFitime> RrugaFitimes { get; set; } = new List<RrugaFitime>();
    public List<PagesaShoferit> PagesaShoferits { get; set; } = new List<PagesaShoferit>();
    public List<PagesaPikaShkarkimit> PagesaPikaShkarkimits { get; set; } = new List<PagesaPikaShkarkimit>();
    public List<PagesaDogana> PagesaDoganas { get; set; } = new List<PagesaDogana>();
    public List<Nafta> Naftas { get; set; } = new List<Nafta>();
    public List<PikaRrugaPagesa> PikaRrugaPagesa { get; set; } = new List<PikaRrugaPagesa>();
}
