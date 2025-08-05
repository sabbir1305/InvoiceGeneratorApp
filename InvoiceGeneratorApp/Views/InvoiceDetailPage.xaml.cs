using InvoiceGeneratorApp.Models;
using InvoiceGeneratorApp.Services;

namespace InvoiceGeneratorApp.Views;

[QueryProperty(nameof(SelectedInvoice), "SelectedInvoice")]
public partial class InvoiceDetailPage : ContentPage
{
    private PdfService _pdfService;
    private PrintService _printService;
    private string? _pdfFilePath;

    private Invoice _selectedInvoice;

    public Invoice SelectedInvoice
    {
        get => _selectedInvoice;
        set
        {
            _selectedInvoice = value;
            BindingContext = _selectedInvoice;
        }
    }

    public InvoiceDetailPage()
    {
        InitializeComponent();

        // Resolve services from global ServiceProvider
        _pdfService = MauiProgram.Services.GetRequiredService<PdfService>();
        _printService = MauiProgram.Services.GetRequiredService<PrintService>();
    }

    private async void OnExportPdfClicked(object sender, EventArgs e)
    {
        try
        {
            _pdfFilePath = await _pdfService.GenerateInvoicePdfAsync(SelectedInvoice);
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
