using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BioLab.Models
{
    public class PikaRrugaPagesa
    {
        public int PikaRrugaPagesaId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int PikaRrugaId { get; set; }
        public PikaRruga? Pika { get; set; }
        [Precision(18, 2)]
        public decimal Pagesa { get; set; }
        [Display(Name = "Shpenzim/Xhiro")]
        public bool ShpenzimXhiro { get; set; }
        [Display(Name = "Pagesa Kryer")]
        public bool PagesaKryer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
