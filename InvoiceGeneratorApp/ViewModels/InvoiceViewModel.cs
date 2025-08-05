using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceGeneratorApp.Models;
using System.Collections.ObjectModel;

namespace InvoiceGeneratorApp.ViewModels
{
    public partial class InvoiceViewModel : ObservableObject
    {
        // Current Invoice
        [ObservableProperty]
        private Invoice currentInvoice;

        // For adding new item
        [ObservableProperty] private string itemDescription = string.Empty;
        [ObservableProperty] private int itemQuantity;
        [ObservableProperty] private decimal itemPrice;

        // Collection for binding to UI
        public ObservableCollection<InvoiceItem> Items { get; } = new();

        public InvoiceViewModel()
        {
            // Initialize new invoice
            currentInvoice = new Invoice
            {
                InvoiceNumber = Invoice.GenerateInvoiceNumber(),
                Date = DateTime.Now
            };
        }

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(ItemDescription) || ItemQuantity <= 0 || ItemPrice <= 0)
                return;

            var newItem = new InvoiceItem
            {
                Description = ItemDescription,
                Quantity = ItemQuantity,
                Price = ItemPrice
            };

            Items.Add(newItem);
            CurrentInvoice.Items.Add(newItem);

            // Clear input fields
            ItemDescription = string.Empty;
            ItemQuantity = 0;
            ItemPrice = 0;
        }

        [RelayCommand]
        private void RemoveItem(InvoiceItem item)
        {
            if (item == null) return;

            Items.Remove(item);
            CurrentInvoice.Items.Remove(item);
        }

        [RelayCommand]
        private void GenerateNewInvoice()
        {
            CurrentInvoice = new Invoice
            {
                InvoiceNumber = Invoice.GenerateInvoiceNumber(),
                Date = DateTime.Now
            };

            Items.Clear();
        }
    }
}
