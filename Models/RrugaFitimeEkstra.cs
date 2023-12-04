using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BioLab.Models
{
    public class RrugaFitimeEkstra
    {
        public int RrugaFitimeEkstraId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int RrugaId { get; set; }
        public Rruga? Rruga { get; set; }
        [Precision(18, 2)]
        public decimal Pagesa { get; set; }
        [Display(Name = "Shpenzim/Xhiro")]
        public bool ShpenzimXhiro { get; set; }
        [Display(Name = "Pagesa Kryer")]
        public bool PagesaKryer { get; set; }
        public string shenime { get; set; }= String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
