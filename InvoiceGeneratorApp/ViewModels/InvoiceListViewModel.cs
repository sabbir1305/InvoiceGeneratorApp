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

        [ObservableProperty]
        private bool isBusy;

        public ObservableCollection<Invoice> Invoices { get; } = new();

        public InvoiceListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadInvoicesCommand.Execute(null);
        }

        [RelayCommand]
        public async Task LoadInvoicesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Invoices.Clear();
                var invoices = await _databaseService.GetInvoicesAsync();
                foreach (var invoice in invoices)
                {
                    Invoices.Add(invoice);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task ViewDetailsAsync(Invoice invoice)
        {
            if (invoice == null) return;

            var detailPage = new InvoiceDetailPage
            {
                SelectedInvoice = invoice
            };

            await Application.Current.MainPage.Navigation.PushAsync(detailPage);
        }


        [RelayCommand]
        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            if (invoice == null) return;

            var confirm = await Shell.Current.DisplayAlert("Delete", "Are you sure?", "Yes", "No");
            if (!confirm) return;

            await _databaseService.DeleteInvoiceAsync(invoice);
            Invoices.Remove(invoice);
        }
    }
}
