using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceGeneratorApp.Models;
public class InvoiceItem
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    [NotMapped]
    public decimal Total => Quantity * Price;

    // Foreign key for Invoice
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
}
