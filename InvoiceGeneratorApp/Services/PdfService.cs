using InvoiceGeneratorApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Colors = QuestPDF.Helpers.Colors;

namespace InvoiceGeneratorApp.Services
{
    public class PdfService
    {
        public async Task<string> GenerateInvoicePdfAsync(Invoice invoice)
        {
            var fileName = $"Invoice_{invoice.InvoiceNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filePath = Path.Combine(folder, fileName);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                    // HEADER with colored bar and company info
                    page.Header()
                        .Height(60)
                        .Background(Colors.Blue.Medium)
                        .Padding(10)
                        .Row(row =>
                        {
                            row.RelativeItem()
                               .Column(col =>
                               {
                                   col.Item().Text("InvoiceGeneratorApp").Bold().FontSize(24).FontColor(Colors.White);
                                   col.Item().Text($"Invoice No: {invoice.InvoiceNumber}").FontSize(14).FontColor(Colors.White);
                               });
                            row.ConstantItem(100)
                               .Image("logo.png"); // Optional logo if available
                        });

                    // CONTENT
                    page.Content()
                        .PaddingVertical(15)
                        .Column(column =>
                        {
                            // Customer Info
                            column.Item().Text("Bill To").Bold().FontSize(16).Underline();
                            column.Item().Text(invoice.CustomerName);
                            column.Item().Text(invoice.CustomerAddress);
                            column.Item().Text(invoice.CustomerEmail);
                            column.Item().Text(invoice.CustomerPhone);
                            column.Item().Text($"Invoice Date: {invoice.Date:dd MMM yyyy}");
                            column.Item().PaddingVertical(10);

                            // ITEMS TABLE with borders and alternating rows
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(5);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(3);
                                });

                                // Header row style
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).BorderBottom(1).BorderColor(Colors.Black).Text("Description").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).BorderBottom(1).BorderColor(Colors.Black).AlignRight().Text("Quantity").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).BorderBottom(1).BorderColor(Colors.Black).AlignRight().Text("Price").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).BorderBottom(1).BorderColor(Colors.Black).AlignRight().Text("Total").Bold();
                                });

                                // Rows with alternating background colors
                                bool isEven = false;
                                foreach (var item in invoice.Items)
                                {
                                    var backgroundColor = isEven ? Colors.White : Colors.Grey.Lighten3;
                                    isEven = !isEven;

                                    table.Cell().Background(backgroundColor).Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Text(item.Description);
                                    table.Cell().Background(backgroundColor).Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten1).AlignRight().Text(item.Quantity.ToString());
                                    table.Cell().Background(backgroundColor).Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten1).AlignRight().Text($"{item.Price:C}");
                                    table.Cell().Background(backgroundColor).Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten1).AlignRight().Text($"{item.Total:C}");
                                }
                            });

                            column.Item().PaddingVertical(15);

                            // Totals section right aligned with spacing and borders
                            column.Item().AlignRight().Column(col =>
                            {
                                col.Item().Text($"Subtotal: {invoice.SubTotal:C}").FontSize(14);
                                col.Item().Text($"Tax (10%): {invoice.Tax:C}").FontSize(14);
                                col.Item().PaddingTop(5).Text($"Total: {invoice.Total:C}").FontSize(18).Bold();
                            });
                        });

                        // FOOTER with page numbering
                        page.Footer()
          .AlignCenter()
          .Text(text =>
          {
              text.Span("Generated by InvoiceGeneratorApp").FontSize(10).FontColor(Colors.Grey.Medium);
              text.Span(" • ").FontSize(10).FontColor(Colors.Grey.Medium);
              text.CurrentPageNumber().FontSize(10).FontColor(Colors.Grey.Medium);
              text.Span(" / ").FontSize(10).FontColor(Colors.Grey.Medium);
              text.TotalPages().FontSize(10).FontColor(Colors.Grey.Medium);
          });
                    });
                });

            using var fs = new FileStream(filePath, FileMode.Create);
            document.GeneratePdf(fs);

            return filePath;
        }
    }
}
