using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Exporters.Contract;

namespace RestWithAspNet10Scaffold.Files.Exporters.Impl;

public class CsvFileExporter : IFileExporter
{
    public FileContentResult ExportFile(List<PersonDto> persons)
    {
        throw new NotImplementedException();
    }
}