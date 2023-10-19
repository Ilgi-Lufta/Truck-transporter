#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class NaftaRruga
{
    [Key]
    public int NaftaRrugaId { get; set; }
    public int RrugaId { get; set; }
    public Rruga? Rruga { get; set; }
    public int NaftaId { get; set; }
    public Nafta? Nafta { get; set; }
    List<PagesaNafta> PagesaNaftas { get; set; } = new List<PagesaNafta>();

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

}