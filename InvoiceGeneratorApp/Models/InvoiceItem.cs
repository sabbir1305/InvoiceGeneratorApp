using System.ComponentModel.DataAnnotations;

namespace InvoiceGeneratorApp.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Total => Quantity * Price;
    }
}
