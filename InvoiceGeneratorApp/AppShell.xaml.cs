using InvoiceGeneratorApp.Views;

namespace InvoiceGeneratorApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(InvoiceDetailPage), typeof(InvoiceDetailPage));


        }
    }
}
