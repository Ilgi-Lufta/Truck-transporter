namespace BioLab.Models
{
    public class PikaRrugaPagesa
    {
        public int PikaRrugaPagesaId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int PikaRrugaId { get; set; }
        public PikaRruga? Pika { get; set; }
        public decimal Pagesa { get; set; }
        public bool ShpenzimXhiro { get; set; }
        public bool PagesaKryer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
