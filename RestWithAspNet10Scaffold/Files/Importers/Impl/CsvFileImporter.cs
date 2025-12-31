using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Importers.Contract;

namespace RestWithAspNet10Scaffold.Files.Importers.Impl;

public class CsvFileImporter : IFileImporter
{
    public async Task<List<PersonDto>> ImportFileAsync(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            IgnoreBlankLines = true
        });
        
        var personsDto = new List<PersonDto>();
        
        await foreach (var record in csv.GetRecordsAsync<dynamic>())
        {
            var person = new PersonDto
            {
                FirstName = record.first_name,
                LastName = record.last_name,
                Address = record.address,
                Gender = record.gender,
                Enabled = bool.Parse(record.enabled)
            };
            personsDto.Add(person);
        }
        
        return personsDto;
    }
}