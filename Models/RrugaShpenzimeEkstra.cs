namespace BioLab.Models
{
    public class RrugaShpenzimeEkstra
    {
        public int RrugaShpenzimeEkstraId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int RrugaId { get; set; }
        public Rruga? Rruga { get; set; }
        public decimal Pagesa { get; set; }
        public bool ShpenzimXhiro { get; set; }
        public bool PagesaKryer { get; set; }
        public string shenime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
