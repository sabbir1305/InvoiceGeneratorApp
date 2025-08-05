using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceGeneratorApp.Models;
using System.Collections.ObjectModel;

namespace InvoiceGeneratorApp.ViewModels
{
    public partial class InvoiceListViewModel : ObservableObject
    {
        public ObservableCollection<Invoice> Invoices { get; } = new();

        [RelayCommand]
        private void AddInvoice(Invoice invoice)
        {
            if (invoice != null)
                Invoices.Add(invoice);
        }

        [RelayCommand]
        private void RemoveInvoice(Invoice invoice)
        {
            if (invoice != null)
                Invoices.Remove(invoice);
        }
    }
}
