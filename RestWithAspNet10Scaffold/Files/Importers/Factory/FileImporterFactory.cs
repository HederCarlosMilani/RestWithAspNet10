using RestWithAspNet10Scaffold.Files.Importers.Contract;
using RestWithAspNet10Scaffold.Files.Importers.Impl;

namespace RestWithAspNet10Scaffold.Files.Importers.Factory;

public class FileImporterFactory(IServiceProvider serviceProvider, ILogger<FileImporterFactory> logger)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<FileImporterFactory> _logger = logger;

    public IFileImporter GetImporter(string fileName)
    {
        _logger.LogDebug("Getting file importer for file: {fileName}", fileName);
        
        if (fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Getting file importer for CSV file: {fileName}", fileName);
            return _serviceProvider.GetRequiredService<CsvFileImporter>();
        }
        else if (fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                 fileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Getting file importer for Excel file: {fileName}", fileName);
            return _serviceProvider.GetRequiredService<ExcelFileImporter>();
        }
        else
        {
            _logger.LogError("File format not supported: {fileName}", fileName);
            throw new NotSupportedException($"File format not supported: {fileName}");
        }
    }
}