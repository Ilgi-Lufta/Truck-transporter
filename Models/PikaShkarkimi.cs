#pragma warning disable CS8618
using BioLab.ViewModels;
using Microsoft.EntityFrameworkCore;
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
    [Precision(18, 2)]
    public decimal Pagesa { get; set; }
    public bool Model { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public List<PikaRruga> PikaRrugas { get; set; } = new List<PikaRruga>();
    public List<PagesaPikaShkarkimit> PagesaPikaShkarkimits { get; set; } = new List<PagesaPikaShkarkimit>();


    [NotMapped]
    public List<PagesaPikaShkarkimitVM> PagesaPikaShkarkimitsVM { get; set; } = new List<PagesaPikaShkarkimitVM>();

}