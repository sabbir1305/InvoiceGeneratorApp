using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGeneratorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register ViewModels
        builder.Services.AddSingleton<ViewModels.InvoiceViewModel>();
        builder.Services.AddSingleton<ViewModels.InvoiceListViewModel>();

        // Register Services (later for PDF & DB)
        builder.Services.AddSingleton<Services.PdfService>();
        builder.Services.AddSingleton<Services.DatabaseService>();

        return builder.Build();
    }
}
