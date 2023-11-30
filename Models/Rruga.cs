#pragma warning disable CS8618
using BioLab.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Rruga
{
    [Key]
    public int RrugaId { get; set; }
    [Required]
    public string Emri { get; set; } // vendoset vetem ne fillim
    //[Required]
    //public decimal Dogana { get; set; } // vendoset vetem ne fillim
    public bool Model { get; set; }

    public string shenime { get; set; } = String.Empty;
    public bool PagesaKryer { get; set; }
    //public decimal shpenzimeEkstra { get; set; }
    //public decimal FitimeEkstra { get; set; }
    //[NotMapped]
    //public decimal NaftaBlereLitra { get; set; } // vendoset cdo here
    //[NotMapped]
    //public decimal NaftaBlereCmim { get; set; } // vendoset cdo here
    //[NotMapped]
    //public decimal PikaPagesa { get; set; } // vendoset cdo here
    public List<Nafta> Nafta { get; set; } = new List<Nafta>();
    [Required]
    public decimal NaftaShpenzuarLitra { get; set; } // vendoset vetem ne fillim
    //public decimal NaftaPerTuShiturLitra { get; set; } 
    //public decimal Shpenzime { get; set; } 
    //public decimal Xhiro { get; set; } 
    //public decimal Fitime { get; set; } 

    //public decimal PagesaShoferit { get; set; } // vendoset vetem ne fillim
    //public int ShoferId { get; set; }
    //public Shofer? Shofer { get; set; }
    //public int PikaShkarkimiId { get; set; }
    //public PikaShkarkimi? PikaShkarkimi { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<ShoferRruga> ShoferRrugas { get; set; } = new List<ShoferRruga>();
    public List<RrugaShpenzimeEkstra> RrugaShpenzimeEkstras { get; set; } = new List<RrugaShpenzimeEkstra>();
    public List<RrugaFitimeEkstra> RrugaFitimeEkstras { get; set; } = new List<RrugaFitimeEkstra>();
    public List<RrugaFitime> RrugaFitimes { get; set; } = new List<RrugaFitime>(); // to be calculated
    public List<PikaRruga> PikaRrugas { get; set; } = new List<PikaRruga>();
   // public List<PagesaShoferit> PagesaShoferits { get; set; } = new List<PagesaShoferit>();
    //public List<PagesaPikaShkarkimit> PagesaPikaShkarkimits { get; set; } = new List<PagesaPikaShkarkimit>();
    public List<PagesaDogana> PagesaDoganas { get; set; } = new List<PagesaDogana>();
   // public List<NaftaRruga> NaftaRrugas { get; set; } = new List<NaftaRruga>();

    [NotMapped]
    public int PagesaShoferitCurrencyId { get; set; }
    //[NotMapped]
    //public ICollection<PagesaShoferitVM> PagesaShoferitVM { get; set; }
    [NotMapped]
    public List<PagesaDoganaVM> PagesaDoganaVM { get; set; } = new List<PagesaDoganaVM>();
    [NotMapped]
    public List<ShoferitRrugaVM> ShoferitRrugaVM { get; set; } = new List<ShoferitRrugaVM>();

    [NotMapped]
    public List<PikaRrugasVM> PikaRrugasVM { get; set; } = new List<PikaRrugasVM>();




}