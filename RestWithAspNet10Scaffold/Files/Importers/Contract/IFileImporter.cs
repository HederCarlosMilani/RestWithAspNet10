using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Files.Importers.Contract;

public interface IFileImporter
{
    Task<List<PersonDto>> ImportFileAsync(Stream fileStream);
}