using InvoiceGeneratorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGeneratorApp.Services
{
    public class DatabaseService
    {
        private readonly AppDbContext _context;

        public DatabaseService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public async Task SaveInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice>> GetInvoicesAsync()
        {
            return await _context.Invoices.Include(i => i.Items).ToListAsync();
        }

        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
