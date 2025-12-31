using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Importers.Contract;

namespace RestWithAspNet10Scaffold.Files.Importers.Impl;

public class ExcelFileImporter: IFileImporter
{
    public Task<List<PersonDto>> ImportFileAsync(Stream fileStream)
    {
        throw new NotImplementedException();
    }
}