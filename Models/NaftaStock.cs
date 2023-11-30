#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class NaftaStock
{
    [Key]
    public int NaftaStockId { get; set; }
    [Required]
    public Decimal Litra { get; set; }
    [Required]
    public string BlereShiturSelect { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public decimal Pagesa { get; set; }
    public bool ShpenzimXhiro { get; set; }
    public bool PagesaKryer { get; set; }
    public int? RrugaId { get; set; }
    public Rruga? Rruga { get; set; }
    public int? BlereShiturId { get; set; }
    public BlereShitur? BlereShitur { get; set; }
    public string Shenime { get; set; } = String.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    [NotMapped]
    public decimal Cmimi { get; set; }
}