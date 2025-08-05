using InvoiceGeneratorApp.Models;
using InvoiceGeneratorApp.Services;

namespace InvoiceGeneratorApp.Views;

public partial class InvoiceDetailPage : ContentPage
{
    private readonly Invoice _invoice;
    private readonly PdfService _pdfService;
    private readonly PrintService _printService;

    private string? _pdfFilePath;


    public InvoiceDetailPage(Invoice selectedInvoice, PdfService pdfService, PrintService printService)
    {
        InitializeComponent();
        _invoice = selectedInvoice;
        BindingContext = _invoice;
        _pdfService = pdfService;
        _printService = printService;
    }

    private async void OnExportPdfClicked(object sender, EventArgs e)
    {
        try
        {
            _pdfFilePath = await _pdfService.GenerateInvoicePdfAsync(_invoice);
            await DisplayAlert("PDF Exported", $"Invoice PDF saved at:\n{_pdfFilePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to export PDF: {ex.Message}", "OK");
        }
    }

    private async void OnPrintClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_pdfFilePath))
        {
            await DisplayAlert("Print", "Please export the PDF first.", "OK");
            return;
        }

        try
        {
            await _printService.PrintPdfAsync(_pdfFilePath);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Print Error", $"Failed to print: {ex.Message}", "OK");
        }
    }
}
