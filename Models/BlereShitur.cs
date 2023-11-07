namespace BioLab.Models
{
    public class BlereShitur
    {
        public int BlereShiturId { get; set; }
        public List<Nafta> Nafta { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
