#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Nafta
{
    [Key]
    public int NaftaId { get; set; }
    [Required]
    public Decimal Litra { get; set; }
    [Required]
    public Decimal Cmimi { get; set; }
    [Required]
    public string BlereShiturSelect { get; set; }
    public decimal Leke { get; set; }
    public int? RrugaId { get; set; }
    public Rruga? Rruga { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public List<NaftaRruga> NaftaRrugas { get; set; } = new List<NaftaRruga>();

}