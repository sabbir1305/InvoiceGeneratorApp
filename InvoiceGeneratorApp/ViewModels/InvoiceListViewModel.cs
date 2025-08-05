using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceGeneratorApp.Models;
using InvoiceGeneratorApp.Services;
using InvoiceGeneratorApp.Views;
using System.Collections.ObjectModel;

namespace InvoiceGeneratorApp.ViewModels
{
    public partial class InvoiceListViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Invoice> Invoices { get; } = new();

        public InvoiceListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        public async Task LoadInvoicesAsync()
        {
            Invoices.Clear();
            var invoices = await _databaseService.GetInvoicesAsync();
            foreach (var invoice in invoices)
                Invoices.Add(invoice);
        }

        [RelayCommand]
        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            if (invoice == null) return;
            await _databaseService.DeleteInvoiceAsync(invoice);
            Invoices.Remove(invoice);
        }

        [RelayCommand]
        private async Task OpenInvoiceDetailAsync(Invoice invoice)
        {
            if (invoice == null) return;
            await Application.Current.MainPage.Navigation.PushAsync(new InvoiceDetailPage(invoice));
        }

    }
}
