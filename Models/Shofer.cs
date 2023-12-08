#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Shofer
{
    [Key]
    public int ShoferId { get; set; } 
    [Required]
    public string Emri { get; set; }
    //[Required]
    //[Precision(18, 2)]
    //public decimal Pagesa { get; set; }
    public bool Model { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public List<ShoferRruga> shoferRrugas { get; set; } = new List<ShoferRruga>();

}