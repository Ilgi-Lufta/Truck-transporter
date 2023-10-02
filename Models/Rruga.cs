#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Rruga
{
    [Key]
    public int RrugaId { get; set; }
    [Required]
    public string Emri { get; set; } // vendoset vetem ne fillim
    [Required]
    public decimal Dogana { get; set; } // vendoset vetem ne fillim
    public bool Model { get; set; }

    public string? shenime { get; set; }
    public decimal shpenzimeEkstra { get; set; }
    public decimal FitimeEkstra { get; set; }
    [NotMapped]
    public decimal NaftaBlereLitra { get; set; } // vendoset cdo here
    [NotMapped]
    public decimal NaftaBlereCmim { get; set; } // vendoset cdo here
    [NotMapped]
    public decimal PikaPagesa { get; set; } // vendoset cdo here

    public Nafta? Nafta { get; set; }
    [Required]
    public decimal NaftaShpenzuarLitra { get; set; } // vendoset vetem ne fillim



    public decimal NaftaPerTuShiturLitra { get; set; } 


    public decimal Shpenzime { get; set; } 
    public decimal Xhiro { get; set; } 
    public decimal Fitime { get; set; } 

    public decimal PagesaShoferit { get; set; } // vendoset vetem ne fillim
    public int ShoferId { get; set; }
    public Shofer? Shofer { get; set; }
    public int PikaShkarkimiId { get; set; }
    public PikaShkarkimi? PikaShkarkimi { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}