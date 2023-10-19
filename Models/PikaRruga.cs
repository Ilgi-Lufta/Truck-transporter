#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class PikaRruga
{
    [Key]
    public int PikaRrugaId { get; set; }
    public int PikaShkarkimiId { get; set; }
    public PikaShkarkimi? PikaShkarkimi { get; set; }
    public int RrugaId { get; set; }
    public Rruga? Rruga { get; set; }
    public int PagesaPikaShkarkimitId { get; set; }
    List<PagesaPikaShkarkimit> PagesaPikaShkarkimit { get; set; } = new List<PagesaPikaShkarkimit>();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}