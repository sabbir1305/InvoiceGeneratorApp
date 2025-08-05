using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Maui;

namespace InvoiceGeneratorApp;
public static class MauiProgram
{
    public static IServiceProvider Services; // ✅ Add this
    public static MauiApp CreateMauiApp()
    {

        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        }).UseMauiCommunityToolkit();
        // Register ViewModels
        builder.Services.AddSingleton<ViewModels.InvoiceViewModel>();
        builder.Services.AddSingleton<ViewModels.InvoiceListViewModel>();
        // Register Services (later for PDF & DB)
        builder.Services.AddSingleton<Services.PdfService>();
        builder.Services.AddSingleton<Services.DatabaseService>();
        builder.Services.AddSingleton<Services.PrintService>();

        var app = builder.Build();

        Services = app.Services; // ✅ Store it here

        return app;
    }
}