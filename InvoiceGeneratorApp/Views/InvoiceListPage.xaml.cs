using InvoiceGeneratorApp.ViewModels;

namespace InvoiceGeneratorApp.Views;

public partial class InvoiceListPage : ContentPage
{
    private readonly InvoiceListViewModel _viewModel;
    private readonly InvoiceViewModel _invoiceViewModel;

    public InvoiceListPage(InvoiceListViewModel listViewModel, InvoiceViewModel invoiceViewModel)
    {
        InitializeComponent();
        _viewModel = listViewModel;
        _invoiceViewModel = invoiceViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadInvoicesAsync();
    }

    private async void OnCreateInvoiceClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InvoicePage(_invoiceViewModel));
    }
}
