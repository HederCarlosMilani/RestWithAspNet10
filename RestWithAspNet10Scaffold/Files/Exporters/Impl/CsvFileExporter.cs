using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Exporters.Contract;
using RestWithAspNet10Scaffold.Files.Exporters.Factory;

namespace RestWithAspNet10Scaffold.Files.Exporters.Impl;

public class CsvFileExporter : IFileExporter
{
    public FileContentResult ExportFile(List<PersonDto> persons)
    {
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true);

        using var csv = new CsvWriter(
            streamWriter,
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            }
        );
        
        csv.WriteRecords(persons);
        streamWriter.Flush();
        
        var fileBytes = memoryStream.ToArray();

        return new FileContentResult(fileBytes, MediaTypes.ApplicationCsv)
        {
            FileDownloadName = $"Persons_exported_{DateTime.UtcNow:yyyyMMddHHmmss}.csv"
        };
    }
}