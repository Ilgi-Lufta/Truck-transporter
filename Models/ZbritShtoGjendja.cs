using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BioLab.Models
{
    public class ZbritShtoGjendja
    {
        public int ZbritShtoGjendjaId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public string Shenime { get; set; } =string.Empty;
        [Precision(18, 2)]
        public decimal Pagesa { get; set; }
        [Display(Name = "Zbrit/Shto")]
        public string ZbritShtoSelect { get; set; }
        [Display(Name = "Shpenzim/Xhiro")]
        public bool ShpenzimXhiro { get; set; }
        [Display(Name = "Pika Shkarkimit")]

        public int? PikaShkarkimiId { get; set; }
        public PikaShkarkimi? PikaShkarkimi { get; set; }
        [Display(Name = "Shofer")]

        public int? ShoferId { get; set; }
        public Shofer? Shofer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
