namespace BioLab.Models
{
    public class PagesaPikaShkarkimit
    {
        public int PagesaPikaShkarkimitId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int PikaShkarkimiId { get; set; }
        public PikaRruga? Pika { get; set; }
        public decimal Pagesa { get; set; }
        public bool ShpenzimXhiro { get; set; }
        public bool PagesaKryer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
