namespace InvoiceGeneratorApp.Models
{
    public class Invoice
    {
        public int Id { get; set; } // For DB storage later
        public string InvoiceNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

        public List<InvoiceItem> Items { get; set; } = new();

        // Computed properties
        public decimal SubTotal => Items.Sum(i => i.Total);
        public decimal Tax => SubTotal * 0.1m; // 10% tax for now
        public decimal Total => SubTotal + Tax;

        // Random Invoice Number generator (can be moved to a helper later)
        public static string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 6)}";
        }
    }
}
