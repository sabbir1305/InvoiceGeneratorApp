using InvoiceGeneratorApp.Views;
using InvoiceGeneratorApp.ViewModels;

namespace InvoiceGeneratorApp;

public partial class App : Application
{
    public App(InvoiceListViewModel invoiceListViewModel, InvoiceViewModel invoiceViewModel)
    {
        InitializeComponent();

        MainPage = new NavigationPage(new InvoiceListPage(invoiceListViewModel, invoiceViewModel));
    }
}
