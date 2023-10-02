#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioLab.Models;
public class Currency
{
    [Key]
    public int CurrencyId { get; set; }
    public string CurrencyUnit { get; set; }
    public bool Model { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }= DateTime.Now;
}
