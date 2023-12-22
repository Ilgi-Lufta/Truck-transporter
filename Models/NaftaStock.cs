#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class NaftaStock
{
    [Key]
    public int NaftaStockId { get; set; }
    [Required]
    [Precision(18, 2)]
    public decimal Litra { get; set; }
    [Required]
    [Display(Name = "Blere/Shitur")]
    public string BlereShiturSelect { get; set; }
    [Display(Name = "Monedha")]
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    [Precision(18, 2)]
    public decimal Pagesa { get; set; }
    [Display(Name = "Shpenzim/Xhiro")]
    public bool ShpenzimXhiro { get; set; }
    [Display(Name = "Pagesa Kryer")]
    public bool PagesaKryer { get; set; }
    public int? RrugaId { get; set; }
    public Rruga? Rruga { get; set; }
    public int? BlereShiturId { get; set; }
    public BlereShitur? BlereShitur { get; set; }
    public string Shenime { get; set; } = String.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;


    [NotMapped]
    [Precision(18, 2)]
    public decimal Cmimi { get; set; }
    [NotMapped]
    [Display(Name = "Zgjidh Naften qe do te shitet")]

    public int CurrencyIdShitje { get; set; }

}