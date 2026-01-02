using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Files.Exporters.Contract;

public interface IFileExporter
{
    FileContentResult ExportFile(List<PersonDto> persons);
}