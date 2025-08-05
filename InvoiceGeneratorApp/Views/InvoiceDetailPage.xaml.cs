using InvoiceGeneratorApp.Models;

namespace InvoiceGeneratorApp.Views;

public partial class InvoiceDetailPage : ContentPage
{
    public InvoiceDetailPage(Invoice selectedInvoice)
    {
        InitializeComponent();
        BindingContext = selectedInvoice;
    }
}
