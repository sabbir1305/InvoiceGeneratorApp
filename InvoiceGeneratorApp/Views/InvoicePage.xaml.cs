using InvoiceGeneratorApp.ViewModels;

namespace InvoiceGeneratorApp.Views;

public partial class InvoicePage : ContentPage
{
    public InvoicePage(InvoiceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
