﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BioLab.Models
{
    public class RrugaFitime
    {
        public int RrugaFitimeId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int RrugaId { get; set; }
        public Rruga? Rruga { get; set; }
        [Precision(18, 2)]
        public decimal Pagesa { get; set; }
        [Precision(18, 2)]
        [Display(Name = "Pagesa Reale")]
        public decimal PagesaReale { get; set; }
        [Display(Name = "Shpenzim/Xhiro")]
        public bool ShpenzimXhiro { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        [NotMapped]
        public string Emri  { get; set; }

    }
}
