using RestWithAspNet10Scaffold.Files.Exporters.Contract;
using RestWithAspNet10Scaffold.Files.Exporters.Impl;

namespace RestWithAspNet10Scaffold.Files.Exporters.Factory;

public class FileExporterFactory(IServiceProvider serviceProvider, ILogger<FileExporterFactory> logger)
{
    private readonly ILogger<FileExporterFactory> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IFileExporter GetExporter(string acceptHeader)
    {
        if (string.Equals(acceptHeader, MediaTypes.ApplicationExcel, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Getting file exporter for Excel format: {AcceptHeader}", acceptHeader);
            return _serviceProvider.GetRequiredService<ExcelFileExporter>();
        }
        else if (string.Equals(acceptHeader, MediaTypes.ApplicationCsv, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Getting file exporter for CSV format: {AcceptHeader}", acceptHeader);
            return _serviceProvider.GetRequiredService<CsvFileExporter>();
        }
        else
        {
            _logger.LogError("File export format not supported: {AcceptHeader}", acceptHeader);
            throw new NotSupportedException($"File export format not supported: {acceptHeader}");
        }
    }
}