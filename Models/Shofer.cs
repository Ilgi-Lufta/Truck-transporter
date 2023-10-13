#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Shofer
{
    [Key]
    public int ShoferId { get; set; } 
    [Required]
    public string Emri { get; set; }
    [Required]
    public Decimal Pagesa { get; set; }
    List<Rruga> Rrugas { get; set; }= new List<Rruga>();

    public bool Model { get; set; }
    //[NotMapped]
    //public int CurrencyId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public List<ShoferRruga> shoferRrugas { get; set; } = new List<ShoferRruga>();
    public List<PagesaShoferit> PagesaShoferits { get; set; } = new List<PagesaShoferit>();


}