#pragma warning disable CS8618
using BioLab.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Rruga
{
    [Key]
    public int RrugaId { get; set; }
    [Required]
    public string Emri { get; set; }
    public bool Model { get; set; }
    public string shenime { get; set; } = String.Empty;
    [Display(Name = "Pagesa Kryer")]
    public bool PagesaKryer { get; set; }
    public List<Nafta> Nafta { get; set; } = new List<Nafta>();
    [Required]
    [Precision(18, 2)]
    [Display(Name = "Nafta Shpenzuar Litra")]
    public decimal NaftaShpenzuarLitra { get; set; }
    [Precision(18, 2)]
    [Display(Name = "Nafta Blere Litra")]
    public decimal NaftaBlereLitra { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<ShoferRruga> ShoferRrugas { get; set; } = new List<ShoferRruga>();
    public List<RrugaShpenzimeEkstra> RrugaShpenzimeEkstras { get; set; } = new List<RrugaShpenzimeEkstra>();
    public List<RrugaFitimeEkstra> RrugaFitimeEkstras { get; set; } = new List<RrugaFitimeEkstra>();
    public List<RrugaFitime> RrugaFitimes { get; set; } = new List<RrugaFitime>(); // to be calculated
    public List<PikaRruga> PikaRrugas { get; set; } = new List<PikaRruga>();
    public List<PagesaDogana> PagesaDoganas { get; set; } = new List<PagesaDogana>();
   


    [NotMapped]
    public int PagesaShoferitCurrencyId { get; set; }
    [NotMapped]
    public List<PagesaDoganaVM> PagesaDoganaVM { get; set; } = new List<PagesaDoganaVM>();
    [NotMapped]
    public List<ShoferitRrugaVM> ShoferitRrugaVM { get; set; } = new List<ShoferitRrugaVM>();
    [NotMapped]
    public List<PikaRrugasVM> PikaRrugasVM { get; set; } = new List<PikaRrugasVM>();




}