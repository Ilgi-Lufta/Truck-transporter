namespace BioLab.Models
{
    public class PagesaNafta
    {
        public int PagesaNaftaId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int NaftaRrugaId { get; set; }
        public NaftaRruga? NaftaRruga { get; set; }
        public decimal Pagesa { get; set; }
        public bool ShpenzimXhiro { get; set; }
        public bool PagesaKryer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
