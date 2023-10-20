namespace BioLab.Models
{
    public class PagesaShoferit
    {
        public int PagesaShoferitId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int ShoferRrugaId { get; set; }
        public ShoferRruga? Shofer { get; set; }
        public decimal Pagesa { get; set; }
        public bool ShpenzimXhiro { get; set; }
        public bool PagesaKryer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
