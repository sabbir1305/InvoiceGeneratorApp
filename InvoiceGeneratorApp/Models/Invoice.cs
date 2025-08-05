using InvoiceGeneratorApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceGeneratorApp.Models;
public class Invoice
{
    [Key]
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;

    // Navigation property
    public List<InvoiceItem> Items { get; set; } = new();

    [NotMapped]
    public decimal SubTotal => Items.Sum(i => i.Total);
    [NotMapped]
    public decimal Tax => SubTotal * 0.10m;
    [NotMapped]
    public decimal Total => SubTotal + Tax;

    public static string GenerateInvoiceNumber()
    {
        return $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 6)}";
    }
}
