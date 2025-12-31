using ClosedXML.Excel;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Importers.Contract;

namespace RestWithAspNet10Scaffold.Files.Importers.Impl;

public class ExcelFileImporter: IFileImporter
{
    public Task<List<PersonDto>> ImportFileAsync(Stream fileStream)
    {
        var personsDto = new List<PersonDto>();
        
        using var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheets.First();
        var rows = worksheet.RowsUsed().Skip(1); // Skip header row
        
        foreach (var row in rows)
        {
            if (row.Cell(1) == null)
                continue;
            
            var person = new PersonDto
            {
                FirstName = row.Cell(1).GetString(),
                LastName = row.Cell(2).GetString(),
                Address = row.Cell(3).GetString(),
                Gender = row.Cell(4).GetString(),
                Enabled = row.Cell(5).GetBoolean()
            };
            personsDto.Add(person);
        }

        return Task.FromResult(personsDto);
    }
}