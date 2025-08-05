using System;
using System.Threading.Tasks;
#if ANDROID || IOS || MACCATALYST || WINDOWS
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;
#endif

namespace InvoiceGeneratorApp.Services
{
    public class PrintService
    {
        public async Task PrintPdfAsync(string filePath)
        {
#if WINDOWS
            // Windows: use PrintManager or launcher to print the PDF file
            await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
#elif ANDROID
            // Android printing
            // Use native Android PrintManager or just open the file (simplified)
            await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
#elif IOS || MACCATALYST
            // iOS/Mac Catalyst printing via UIDocumentInteractionController (complex, native)
            await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
#else
            throw new NotSupportedException("Printing not supported on this platform.");
#endif
        }
    }
}
