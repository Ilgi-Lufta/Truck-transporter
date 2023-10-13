namespace BioLab.Models
{
    public class ZbritShtoGjendja
    {
        public int ZbritShtoGjendjaId { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public string Shenime { get; set; } =string.Empty;
        public decimal Pagesa { get; set; }
        public bool ShpenzimXhiro { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
