using InvoiceGeneratorApp.Views;
using InvoiceGeneratorApp.ViewModels;

namespace InvoiceGeneratorApp;

public partial class App : Application
{
    public App(InvoiceViewModel invoiceViewModel)
    {
        InitializeComponent();

        MainPage = new NavigationPage(new InvoicePage(invoiceViewModel));
    }
}
