#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class PikaShkarkimi
{
    [Key]
    public int PikaShkarkimiId { get; set; }
    [Required]
    public string Emri { get; set; }
    [Required]
    public Decimal Pagesa { get; set; }
    List<Rruga> Rrugas { get; set; }= new List<Rruga>();
    public bool Model { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}