#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class ShoferRruga
{
    [Key]
    public int ShoferRrugaId { get; set; }
    public int ShoferId { get; set; }
    public Shofer? Shofer { get; set; }
    public int RrugaId { get; set; }
    public Rruga? Rruga { get; set; }
    public List<PagesaShoferit> PagesaShoferits { get; set; } = new List<PagesaShoferit>();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}