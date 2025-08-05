namespace InvoiceGeneratorApp.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; } // For DB storage later
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;
    }
}
