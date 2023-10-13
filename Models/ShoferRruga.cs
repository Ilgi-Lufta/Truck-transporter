#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class ShoferRruga
{
    [Key]
    public int ShoferRrugaId { get; set; }
    public int PikaShkarkimiId { get; set; }
    public PikaShkarkimi? PikaShkarkimi { get; set; }
    public int RrugaId { get; set; }
    public Rruga? Rruga { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}